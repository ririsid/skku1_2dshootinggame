using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SecurePlayerPrefs
{
    // 암호화에 사용될 비밀 키 (실제 사용 시 변경 필요)
    private static readonly string secretKey = "MySecretKey123!";

    // 솔트 값 (무작위 대입 공격 방지용, 실제 사용 시 변경 필요)
    private static readonly byte[] salt = new byte[] { 11, 22, 33, 44, 55, 66, 77, 88 };

    // 암호화 키 생성 메서드
    private static byte[] GetKey()
    {
        // Rfc2898DeriveBytes를 사용하여 비밀 키와 솔트로부터 암호화 키 생성
        using (var deriveBytes = new Rfc2898DeriveBytes(secretKey, salt, 1000))
        {
            return deriveBytes.GetBytes(256 / 8); // 32바이트(256비트) 키 반환
        }
    }

    // 문자열 값을 암호화하여 PlayerPrefs에 저장
    public static void SetString(string key, string value)
    {
        string encryptedKey = EncryptKey(key); // 해시 기반 키 암호화
        string encryptedValue = EncryptString(value); // IV 포함 값 암호화
        PlayerPrefs.SetString(encryptedKey, encryptedValue); // 암호화된 키-값 쌍 저장
    }

    // PlayerPrefs에서 암호화된 값을 가져와 복호화
    public static string GetString(string key, string defaultValue = "")
    {
        string encryptedKey = EncryptKey(key); // 해시 기반 키 암호화
        string encryptedValue = PlayerPrefs.GetString(encryptedKey, ""); // 암호화된 값 가져오기
        // 값이 없으면 기본값 반환, 있으면 복호화하여 반환
        return string.IsNullOrEmpty(encryptedValue) ? defaultValue : DecryptString(encryptedValue);
    }

    public static bool HasKey(string key)
    {
        string encryptedKey = EncryptKey(key); // 해시 기반 키 암호화
        return PlayerPrefs.HasKey(encryptedKey); // 암호화된 키가 존재하는지 확인
    }

    // PlayerPrefs에 저장된 값을 삭제
    public static void DeleteKey(string key)
    {
        string encryptedKey = EncryptKey(key); // 해시 기반 키 암호화
        PlayerPrefs.DeleteKey(encryptedKey); // 암호화된 키 삭제
    }

    // 해시 기반 키 암호화 메서드
    // 같은 키로 암호화하면 같은 결과가 나오도록 함
    private static string EncryptKey(string key)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(key + secretKey));
            return Convert.ToBase64String(hashBytes);
        }
    }

    // 문자열 암호화 메서드
    private static string EncryptString(string input)
    {
        byte[] encrypted;
        byte[] iv;
        using (Aes aes = Aes.Create()) // AES 암호화 알고리즘 사용
        {
            aes.Key = GetKey(); // 암호화 키 설정
            aes.GenerateIV(); // 초기화 벡터(IV) 생성
            iv = aes.IV; // 초기화 벡터 저장

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(input); // 입력 문자열 암호화
                }
                encrypted = msEncrypt.ToArray(); // 암호화된 바이트 배열
            }
        }

        // IV와 암호화된 데이터를 결합
        byte[] combinedIvCt = new byte[iv.Length + encrypted.Length];
        Array.Copy(iv, 0, combinedIvCt, 0, iv.Length);
        Array.Copy(encrypted, 0, combinedIvCt, iv.Length, encrypted.Length);

        return Convert.ToBase64String(combinedIvCt); // Base64 인코딩하여 반환
    }

    // 암호화된 문자열 복호화 메서드
    private static string DecryptString(string cipherText)
    {
        byte[] combinedIvCt = Convert.FromBase64String(cipherText);
        byte[] iv = new byte[16];
        byte[] encrypted = new byte[combinedIvCt.Length - iv.Length];

        // IV와 암호화된 데이터 분리
        Array.Copy(combinedIvCt, iv, iv.Length);
        Array.Copy(combinedIvCt, iv.Length, encrypted, 0, encrypted.Length);

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKey(); // 복호화 키 설정
            aes.IV = iv; // 초기화 벡터 설정

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var msDecrypt = new MemoryStream(encrypted))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd(); // 복호화된 문자열 반환
            }
        }
    }
}

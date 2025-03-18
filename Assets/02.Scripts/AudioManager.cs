using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("AudioManager");
                instance = obj.AddComponent<AudioManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Range(0f, 1f)]
    [SerializeField] private float bgmVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // 오디오 소스 컴포넌트 생성
        if (bgmSource == null)
        {
            GameObject bgmObj = new GameObject("BGM_Source");
            bgmObj.transform.parent = transform;
            bgmSource = bgmObj.AddComponent<AudioSource>();
            bgmSource.loop = true;
        }

        if (sfxSource == null)
        {
            GameObject sfxObj = new GameObject("SFX_Source");
            sfxObj.transform.parent = transform;
            sfxSource = sfxObj.AddComponent<AudioSource>();
        }
    }

    // 오디오 클립 등록
    public void RegisterAudioClip(string clipName, AudioClip clip)
    {
        if (!audioClips.ContainsKey(clipName))
        {
            audioClips.Add(clipName, clip);
        }
    }

    // BGM 재생
    public void PlayBGM()
    {
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    // 효과음 재생
    public void PlaySFX(string clipName)
    {
        if (audioClips.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
        else
        {
            Debug.LogWarning($"등록되지 않은 효과음입니다: {clipName}");
        }
    }

    // 오디오 클립 직접 재생
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }

    // BGM 정지
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // BGM 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    // 효과음 볼륨 설정
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    // 모든 소리 정지
    public void StopAllSounds()
    {
        bgmSource.Stop();
        sfxSource.Stop();
    }
}
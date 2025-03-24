using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // "응집도"는 높히고! "결합도"는 낮춰라
    // 응집도: 데이터와 데이터를 조작하는 로직이 한 곳에 모여 있는 구조 -> 응집도가 높은 구조
    // 결합도: 두 클래스간에 상호작용 의존 정도
    // 내 코드가 그렇게 이상한가요?(C#) -> 도메인 주도 설계 철저 입문(C#)


    public int Health = 100;
    public float MoveSpeed = 3f;
    public float AttackCooltime = 0.6f;

    public float Defense = 0.2f;


    // - 모드(자동, 수동)
    public PlayMode PlayMode = PlayMode.Manual;

    public PlayerData PlayerData = new PlayerData();
    public bool HasBoom => PlayerData.BoomCount > 0;

    private CameraShake _cameraShake;

    private const int MAX_COUNT = 3;
    private const int ADD_COUNT = 20;
    private const int BOSS_SPAWN_COUNT = 100;

    private int _killCountForBoom
    {
        get => PlayerData.KillCount % ADD_COUNT;
    }

    private bool _isReset = true;

    private bool _isBossModeForTest = true;

    private void Start()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        if (_isReset)
        {
            SecurePlayerPrefs.DeleteKey("PlayerData");

        }
        if (_isBossModeForTest)
        {
            PlayerData.KillCount = 99;
        }
        else
        {
            Load();
        }
        UI_Game.Instance.Refresh(PlayerData.BoomCount, PlayerData.KillCount);
        UI_Game.Instance.RefreshScore(PlayerData.Score);
    }

    private void Save()
    {
        // PlayerPrefs: 값을 키(Key)와 값(Value) 형태로 저장하는 클래스
        // 저장할 수 있는 자료형은 int, float, string입니다.

        // 각각 쌍으로 저장(Set), 불러오기(Get)함수가 있다.

        string data = JsonUtility.ToJson(PlayerData);
        SecurePlayerPrefs.SetString("PlayerData", data);
    }

    private void Load()
    {
        string jsonString = SecurePlayerPrefs.GetString("PlayerData");
        if (string.IsNullOrEmpty(jsonString) == false)
        {
            PlayerData = JsonUtility.FromJson<PlayerData>(jsonString);
        }
    }

    public void AddScore(int score)
    {
        PlayerData.Score += score;

        UI_Game.Instance.RefreshScore(PlayerData.Score);

        Save();
    }

    public void AddKillCount()
    {
        PlayerData.KillCount++;

        if (PlayerData.KillCount > 0 && _killCountForBoom == 0)
        {
            PlayerData.BoomCount = Mathf.Min(PlayerData.BoomCount + 1, MAX_COUNT);
        }

        UI_Game.Instance.Refresh(PlayerData.BoomCount, PlayerData.KillCount);

        Save();

        if (PlayerData.KillCount > 0 && PlayerData.KillCount % BOSS_SPAWN_COUNT == 0)
        {
            BossSpawner.Instance.Spawn();
        }
    }

    public void SubtractBoomCount()
    {
        PlayerData.BoomCount -= 1;

        UI_Game.Instance.Refresh(PlayerData.BoomCount, PlayerData.KillCount);

        Save();
    }

    public void TakeDamage(int damage)
    {
        Health -= (int)(damage * Defense);

        _cameraShake.Shake();

        if (Health <= 0)
        {
            ResetData();
            Destroy(this.gameObject);
        }
    }

    private void ResetData()
    {
        SecurePlayerPrefs.DeleteKey("PlayerData");
    }

    public void EatAllItems()
    {
        var itemObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (var itemObject in itemObjects)
        {
            itemObject.GetComponent<ItemObject>().IsMagnetized = true;
        }
    }

    public void HoldFire()
    {
        var playerFire = GetComponent<PlayerFire>();
        playerFire.HoldFire();
    }

    public void ResumeFire()
    {
        var playerFire = GetComponent<PlayerFire>();
        playerFire.ResumeFire();
    }

    private void Update()
    {
        // 키 입력 검사
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayMode = PlayMode.Auto;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayMode = PlayMode.Manual;
        }
    }
}

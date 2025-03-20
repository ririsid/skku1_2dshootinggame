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

    private PlayerData MyData = new PlayerData();
    public bool HasBoom => MyData.BoomCount > 0;

    private CameraShake _cameraShake;

    private const int MAX_COUNT = 3;
    private const int ADD_COUNT = 20;

    private void Start()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        Load();
        UI_Game.Instance.Refresh(MyData.BoomCount, MyData.KillCount);
        UI_Game.Instance.RefreshScore(MyData.Score);
    }

    private void Save()
    {
        // PlayerPrefs: 값을 키(Key)와 값(Value) 형태로 저장하는 클래스
        // 저장할 수 있는 자료형은 int, float, string입니다.

        // 각각 쌍으로 저장(Set), 불러오기(Get)함수가 있다.

        string data = JsonUtility.ToJson(MyData);
        SecurePlayerPrefs.SetString("PlayerData", data);
    }

    private void Load()
    {
        string jsonString = SecurePlayerPrefs.GetString("PlayerData");
        if (string.IsNullOrEmpty(jsonString) == false)
        {
            MyData = JsonUtility.FromJson<PlayerData>(jsonString);
        }
    }

    public void AddScore(int score)
    {
        MyData.Score += score;

        UI_Game.Instance.RefreshScore(MyData.Score);

        Save();
    }

    public void AddKillCount()
    {
        MyData.KillCount++;

        if (MyData.KillCount >= ADD_COUNT)
        {
            MyData.KillCount = 0;
            MyData.BoomCount = Mathf.Min(MyData.BoomCount + 1, MAX_COUNT);
        }

        UI_Game.Instance.Refresh(MyData.BoomCount, MyData.KillCount);

        Save();
    }

    public void SubstractBoomCount()
    {
        MyData.BoomCount -= 1;

        UI_Game.Instance.Refresh(MyData.BoomCount, MyData.KillCount);

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

using UnityEngine;

public class Player : MonoBehaviour
{
    // "응집도"는 높히고! "결합도"는 낮춰라
    // 응집도: 데이터와 데이터를 조작하는 로직이 한 곳에 모여 있는 구조 -> 응집도가 높은 구조
    // 결합도: 두 클래스간에 상호작용 의존 정도
    // 내 코드가 그렇게 이상한가요?(C#) -> 도메인 주도 설계 철저 입문(C#)

    public PlayerData PlayerData;

    public bool SaveInit = false;
    
    public int   Health = 100;
    public float MoveSpeed => StatManager.Instance.Stats[(int)StatType.MoveSpeed].Value;
    public float AttackCooltime  = 0.6f;

    public float Defence = 0.2f;
    
    
    // - 모드(자동, 수동)
    public PlayMode PlayMode = PlayMode.Mannual;


    private void Start()
    {
        Health = (int)StatManager.Instance.Stats[(int)StatType.Health].Value;
        
        Load();
        
        UI_Game.Instance.RefreshScore(PlayerData.Score);
    }
    
    private void Save()
    {
        // Json 직렬화
        string jsonString = JsonUtility.ToJson(PlayerData);
        
        // 저장
        PlayerPrefs.SetString("PlayerData", jsonString);
    }

    private void Load()
    {
        if (SaveInit)
        {
            PlayerPrefs.DeleteKey("PlayerData");
        }
        
        // 로드
        string jsonLoadedString = PlayerPrefs.GetString("PlayerData", string.Empty);
        
        // Json 역직렬화
        if (!string.IsNullOrEmpty(jsonLoadedString))
        {
            PlayerData = JsonUtility.FromJson<PlayerData>(jsonLoadedString);
        }
        else
        {
            PlayerData = new PlayerData();
        }
    }
    

    // 플레이어 vs 관리자 vs UI
    public void AddScore(int score)
    {
        PlayerData.Score += score;

        UI_Game.Instance.RefreshScore(PlayerData.Score);

        Save();
    }
    
    public void TakeDamage(int damage)
    {
        Health -= (int)(damage * Defence);

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // 키 입력 검사
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayMode = PlayMode.Auto;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayMode = PlayMode.Mannual;
        }

    }
}

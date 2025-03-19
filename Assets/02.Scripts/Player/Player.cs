using UnityEngine;

public class PlayerData
{
    public int Score = 0;
    public int KillCount = 0;
    public int BoomCount = 0;
}

public class Player : MonoBehaviour
{
    // "응집도"는 높히고! "결합도"는 낮춰라
    // 응집도: 데이터와 데이터를 조작하는 로직이 한 곳에 모여 있는 구조 -> 응집도가 높은 구조
    // 결합도: 두 클래스간에 상호작용 의존 정도
    // 내 코드가 그렇게 이상한가요?(C#) -> 도메인 주도 설계 철저 입문(C#)
    
    
    public int   Health = 100;
    public float MoveSpeed = 3f;
    public float AttackCooltime  = 0.6f;

    public float Defence = 0.2f;

    public UI_Game GameUI;

    
    // - 모드(자동, 수동)
    public PlayMode PlayMode = PlayMode.Mannual;


    private void Start()
    {
        Load();
        GameUI.RefreshScore(_score);
    }
    
    private void Save()
    {
        // PlayerPrefs: 값을 키(Key)와 값(Value) 형태로 저장하는 클래스
        // 저장할 수 있는 자료형은 int, float, string입니다.
        
        // 각각 쌍으로 저장(Set), 불러오기(Get)함수가 있다.
        
        PlayerPrefs.SetInt("Score", _score);
        PlayerPrefs.SetInt("Boom", _score);
        PlayerPrefs.SetInt("KIllCount", _score);
    }

    private void Load()
    {
        _score = PlayerPrefs.GetInt("Score", 0);
    }
    

    // 플레이어 vs 관리자 vs UI
    public void AddScore(int score)
    {
        _score += score;
        
        GameUI.RefreshScore(_score);

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

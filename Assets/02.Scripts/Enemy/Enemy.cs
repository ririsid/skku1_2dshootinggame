using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    Basic = 0,
    Target = 1,
    Follow = 2,
    Boss = 3,
}


public class Enemy : MonoBehaviour
{
    [Header("적 타입")]
    public EnemyDataSO Data;

    public float Speed;
    public int MaxHealth;
    public int Damage;

    private GameObject _player = null; // 저 널널해요.
    private Vector2 _direction;

    private Animator _animator;

    public GameObject[] ItemPrefabs;
    public GameObject ExplosionVFXPrefab;

    public int Health;

    public void Initialize()
    {
        LevelDataSO levelData = LevelManager.Instance.GetLevelData();
        Damage = (int)(Data.Damage * levelData.DamageFactor);
        MaxHealth = (int)(Data.MaxHealth * levelData.HealthFactor);
        Speed = Data.Speed * levelData.SpeedFactor;

        Health = MaxHealth;
    }

    private void Awake()
    {
        Initialize();
        _animator = GetComponent<Animator>();
    }


    private void Start()
    {
        switch (Data.EnemyType)
        {
            case EnemyType.Basic:
                {
                    _direction = Vector2.down;
                    break;
                }

            case EnemyType.Target:
                {
                    SetDirection();
                    break;
                }

        }
    }

    private void OnDestroy()
    {
        if (!SceneManager.GetActiveScene().isLoaded) return;

        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            AudioManager.Instance.PlaySFX(audioSource.clip);
        }
    }

    // 함수란: 반복해서 사용할 코드를 이름 하나로 만들어 놓은 코드의 집
    private void SetDirection()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        if (_player == null) return;

        // 방향을 구한다. (target - me)
        _direction = _player.transform.position - this.transform.position;
        _direction.Normalize(); // 정규화

        float radian = Mathf.Atan2(_direction.y, _direction.x);
        // radian = Mathf.Atan(_direction.y / _direction.x);
        float angle = Mathf.Rad2Deg * radian + 90;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }



    // 매프레임마다 자동으로 호출되는 함수
    private void Update()
    {
        // 타겟형이 매번 방향을 갱신
        if (Data.EnemyType == EnemyType.Follow)
        {
            SetDirection();
        }

        //transform.Translate(_direction * Speed * Time.deltaTime);
        // Translate 조향이 필요할 때 쓰는게 좋다.
        transform.position += (Vector3)_direction * Speed * Time.deltaTime;
    }

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;

        if (Health <= 0)
        {
            OnDeath(damage);
            gameObject.SetActive(false);
        }
        else
        {
            _animator.SetTrigger("Hit");
        }
    }
    // 죽었을때 호출되는 함수
    private void OnDeath(Damage damage)
    {
        // 폭발 이펙트를 생성
        VfxPool.Instance.Create(ExplosionVFXPrefab.name, this.transform.position);

        // 플레이어 붐에게 나 죽었음을 알린다.
        if (damage.Type == DamageType.Bullet)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Player>().AddKillCount();
            player.GetComponent<Player>().AddScore(Data.Score);
            if (Data.EnemyType == EnemyType.Boss)
            {
                BossSpawner.Instance.RemoveBoss();
            }
        }

        // 30% 확률로
        if (Random.Range(0f, 1f) < 0.3f)
        {
            // 랜덤한 아이템 생성
            GameObject itemObject = Instantiate(ItemPrefabs[Random.Range(0, ItemPrefabs.Length)]);
            itemObject.transform.position = this.transform.position;
        }
    }




    // 충돌 이벤트 함수
    // - Trigger 이벤트    : 물리 연산을 무시하지만, 충돌 이벤트를 받겠다.
    // - Collision 이벤트  : 물리 연산도 하고, 충돌 이벤트도 받겠다.

    // 충돌 시작, 충돌 중, 충돌 끝

    // 다른 콜라이더와 충돌이 일어났을때 자동으로 호출되는 함수
    private void OnTriggerEnter2D(Collider2D other)   // Stay(충돌중), Exit(충돌끝)
    {

        // 플레이어와 충돌:
        if (other.CompareTag("Player"))
        {
            // 플레이어 체력이 0 이하일때만 죽인다.
            Player player = other.GetComponent<Player>(); // 게임 오브젝트의 컴포넌트를 가져온다.

            // 묻지말고 시켜라!
            player.TakeDamage(Damage);

            // 나죽자
            gameObject.SetActive(false);
        }

    }
}

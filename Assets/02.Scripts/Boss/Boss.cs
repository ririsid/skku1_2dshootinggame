using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


enum BossMoveState
{
    MoveToDestination,
    MoveAroundDestination
}

enum BossAngryLevel
{
    Level1,
    Level2,
    Level3
}

public class Boss : Enemy
{
    public Vector3 LandingPosition = new Vector3(0f, 2f, 0f);

    public GameObject[] BulletPrefabs;

    private BossMoveState _moveState = BossMoveState.MoveToDestination;

    private BossAngryLevel _angryState = BossAngryLevel.Level1;

    private readonly float _movementRadius = 2f;

    private Vector2 _randomPosition = Vector2.zero;

    private bool _isLanding = false;

    private float _circleFireCoolTime = 1f;

    private float _mineFireCoolTime = 3f;

    private bool _isCircleFiring = false;

    private bool _isMineFiring = false;

    private bool _flagFireDirection = false;

    private int _initialHealth;

    private void Start()
    {
        _initialHealth = Health;
    }

    private void Update()
    {
        Move();
        Fire();
        CheckHealth();
    }

    private void Move()
    {
        switch (_moveState)
        {
            case BossMoveState.MoveToDestination:
                MoveToDestination();
                break;
            case BossMoveState.MoveAroundDestination:
                MoveAroundDestination();
                break;
        }
    }

    void MoveToDestination()
    {
        // 보스가 있으면 보스의 위치를 지정된 위치로 이동시킨다.
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, LandingPosition, Speed * Time.deltaTime);
        if (gameObject.transform.position == LandingPosition)
        {
            _isLanding = true;
        }
    }

    void MoveAroundDestination()
    {
        // 보스가 지정된 위치 반경을 계속 돌아다닌다.
        // 랜덤 포지선 근처에 도착하기 전까지는 계속 이동한다.
        if (Vector3.Distance(gameObject.transform.position, _randomPosition) < 0.1f)
        {
            _randomPosition = LandingPosition + Random.insideUnitSphere * _movementRadius;
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _randomPosition, Speed * Time.deltaTime);
    }

    private void Fire()
    {
        if (!_isLanding) return;
        if (!_isCircleFiring)
        {
            StartCoroutine(CircleFireCoroutine());
        }
        if (!_isMineFiring && _angryState == BossAngryLevel.Level3)
        {
            StartCoroutine(MineFireCoroutine());
        }
    }

    private IEnumerator CircleFireCoroutine()
    {
        _isCircleFiring = true;
        switch (_angryState)
        {
            case BossAngryLevel.Level1:
                CircleFireLevel1();
                break;
            case BossAngryLevel.Level2:
                CircleFireLevel2();
                break;
            case BossAngryLevel.Level3:
                CircleFireLevel3();
                break;
        }
        yield return new WaitForSeconds(_circleFireCoolTime);
        _isCircleFiring = false;
    }

    private IEnumerator MineFireCoroutine()
    {
        _isMineFiring = true;
        GameObject bullet = Instantiate(BulletPrefabs[3], transform.position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        yield return new WaitForSeconds(_mineFireCoolTime);
        _isMineFiring = false;
    }

    private void CircleFireLevel1()
    {
        int bulletCount = 20;
        // 총알을 360도로 균등하게 발사한다.
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * 360f / bulletCount;
            GameObject bullet = Instantiate(BulletPrefabs[0], transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void CircleFireLevel2()
    {
        int bulletCount = 20;
        // 총알을 360도로 균등하게 발사한다.
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * 360f / bulletCount;
            GameObject bullet = Instantiate(BulletPrefabs[1], transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        _flagFireDirection = !_flagFireDirection;
    }

    private void CircleFireLevel3()
    {
        StartCoroutine(FireLevel3Coroutine());
    }

    private IEnumerator FireLevel3Coroutine()
    {
        int bulletCount = 7;
        float startAngle = 150f;
        float endAngle = 222f;
        if (_flagFireDirection)
        {
            startAngle = 222f;
            endAngle = 150f;
        }
        _flagFireDirection = !_flagFireDirection;
        // 총알을 부채꼴로 발사한다.
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (endAngle - startAngle) * i / bulletCount;
            GameObject bullet = Instantiate(BulletPrefabs[2], transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
    }

    private void CheckHealth()
    {
        if (_isLanding)
        {
            UI_Game.Instance.UpdateBossHealthSlider(Health);
        }
        switch (Health)
        {
            case var _ when Health <= _initialHealth * 0.3f:
                _isLanding = true;
                _angryState = BossAngryLevel.Level3;
                _moveState = BossMoveState.MoveAroundDestination;
                _circleFireCoolTime = 0.5f;
                break;
            case var _ when Health <= _initialHealth * 0.7f:
                _isLanding = true;
                _angryState = BossAngryLevel.Level2;
                _moveState = BossMoveState.MoveAroundDestination;
                break;
            case var _ when Health <= _initialHealth * 0.9f:
                _isLanding = true;
                _moveState = BossMoveState.MoveAroundDestination;
                break;
        }
    }
}
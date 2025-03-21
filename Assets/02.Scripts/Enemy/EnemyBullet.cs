using DG.Tweening;
using UnityEngine;

public enum EnemyBulletType
{
    BulletType1,
    BulletType2,
    BulletType3,
    BulletType4
}

public class EnemyBullet : MonoBehaviour
{
    public float Speed = 5;

    public EnemyBulletType BulletType = EnemyBulletType.BulletType1;

    public GameObject ExplosionVFXPrefab;

    public int Damage = 10;

    private Vector2 _direction = Vector2.up;

    private Vector3 _startPosition;

    private const float _type4MovementDistance = 4f;

    private float _explosionDelay = 1.5f;

    private const float _explosionDuration = 0.5f;

    private const float _explosionRadius = 1.5f;

    private bool _isMovementStopped = false;

    private bool _isExploding = false;

    private bool _isExploded = false;

    private GameObject _player;

    private GameObject _explosion;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationAmount = 30f; // 최대 회전 각도
    private float elapsedTime = 0f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        Move();

        if (BulletType == EnemyBulletType.BulletType4)
        {
            CheckPosition();
            EvaluateExplosion();
            EvaluateExplosionRadius();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌:
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        if (_isMovementStopped) return;
        if (BulletType == EnemyBulletType.BulletType2)
        {
            // 시간 업데이트
            elapsedTime += Time.deltaTime * rotationSpeed;

            // 사인 함수를 이용한 진동형 회전
            float angle = Mathf.Sin(elapsedTime) * rotationAmount;
            _direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
        }
        transform.Translate(_direction * Speed * Time.deltaTime);
    }

    private void CheckPosition()
    {
        if (_isMovementStopped) return;
        float distance = _type4MovementDistance + Random.Range(0f, 2f);
        if (Vector3.Distance(_startPosition, transform.position) >= distance)
        {
            _isMovementStopped = true;
        }
    }

    private void EvaluateExplosion()
    {
        if (!_isMovementStopped) return;
        _explosionDelay -= Time.deltaTime;
        if (_explosionDelay > 0) return;
        TriggerExplosion();
    }

    private void EvaluateExplosionRadius()
    {
        if (!_isExploding || _isExploded) return;
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null) return;
        if (Vector3.Distance(_player.transform.position, transform.position) > _explosionRadius) return;
        Player player = _player.GetComponent<Player>();
        player.TakeDamage(Damage);
        _isExploded = true;
    }

    private void TriggerExplosion()
    {
        if (_isExploding) return;
        _isExploding = true;
        _explosion = Instantiate(ExplosionVFXPrefab, transform.position, Quaternion.identity);
        _explosion.transform.DOScale(_explosionRadius, _explosionDuration).OnComplete(() =>
        {
            Destroy(_explosion);
            Destroy(gameObject);
        });
    }
}

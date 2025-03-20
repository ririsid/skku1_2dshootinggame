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

    public int Damage = 10;

    private Vector2 _direction = Vector2.up;

    private void Start()
    {
        switch (BulletType)
        {
            case EnemyBulletType.BulletType2:
                _direction = Vector2.up + Vector2.left;
                break;
            default:
                _direction = Vector2.up;
                break;
        }
    }

    void Update()
    {
        Move();
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
        if (BulletType == EnemyBulletType.BulletType2)
        {
            // 조금씩 각도를 틀어서 전진하기
            _direction = Quaternion.Euler(0, 0, -0.5f) * _direction;
        }
        transform.Translate(_direction * Speed * Time.deltaTime);
    }
}

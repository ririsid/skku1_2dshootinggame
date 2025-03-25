using UnityEngine;

public enum BulletType
{
    Main,
    Sub,
    Pet
}

public class Bullet : MonoBehaviour
{
    // 목표: 위로 계속 이동하고 싶다.

    // 필요 속성
    public BulletDataSO Data;

    public void Initialize()
    {

    }

    // 기능
    // - 계속 위로 이동
    private void Move()
    {
        // 구현 순서
        // 1. 방향을 정한다.
        Vector2 dir = Vector2.up;

        // 2. 이동한다.
        transform.Translate(dir * Data.Speed * Time.deltaTime);
    }


    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 총알과 충돌:
        if (other.CompareTag("Enemy"))
        {
            Damage damage = new Damage
            {
                Value = Data.Damage + (int)StatManager.Instance.Stats[(int)StatType.Damage].Value,
                Type = DamageType.Bullet,
            };

            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);

            gameObject.SetActive(false);
        }
    }
}

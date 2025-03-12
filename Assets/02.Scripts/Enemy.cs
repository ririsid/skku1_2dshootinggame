using UnityEngine;

public enum EnemyType
{
    Basic  = 0,
    Target = 1
}

public class Enemy : MonoBehaviour
{
    [Header("적 타입")]
    public EnemyType EnemyType;
    
    public float Speed = 5f;
    public int Health = 100;
    public int Damage = 40;
    
    private Vector2 _direction;

    private void Start()
    {
        switch (EnemyType)
        {
            case EnemyType.Basic:
            {
                _direction = Vector2.down;
                break;
            }

            case EnemyType.Target:
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                // 방향을 구한다. (target - me)
                _direction = player.transform.position - this.transform.position;
                _direction.Normalize(); // 정규화
                break;
            }
                
        }
    }
    

    // 매프레임마다 자동으로 호출되는 함수
    private void Update()
    {
        transform.Translate(_direction * Speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Destroy(this.gameObject);
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
        if(other.CompareTag("Player"))
        {
            // 플레이어 체력이 0 이하일때만 죽인다.
            Player player = other.GetComponent<Player>(); // 게임 오브젝트의 컴포넌트를 가져온다.

            // 묻지말고 시켜라!
            player.TakeDamage(Damage);

            // 나죽자
            Destroy(this.gameObject);
        }

    }
}

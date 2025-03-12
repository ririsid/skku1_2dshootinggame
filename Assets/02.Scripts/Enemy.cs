using UnityEngine;
 

public class Enemy : MonoBehaviour
{
    public float Speed = 5f;
    public int Health = 100;
    public int Damage = 40;

    // 매프레임마다 자동으로 호출되는 함수
    private void Update()
    {
        Vector2 dir = Vector2.down;
        transform.Translate(dir * Speed * Time.deltaTime);
    }


    // 충돌 이벤트 함수
    // - Trigger 이벤트    : 물리 연산을 무시하지만, 충돌 이벤트를 받겠다.
    // - Collision 이벤트  : 물리 연산도 하고, 충돌 이벤트도 받겠다.

    // 충돌 시작, 충돌 중, 충돌 끝

    // 다른 콜라이더와 충돌이 일어났을때 자동으로 호출되는 함수
    private void OnTriggerEnter2D(Collider2D other)   // Stay(충돌중), Exit(충돌끝)
    {
        // 총알과 충돌:
        if(other.CompareTag("Bullet"))
        {
            // 너죽고
            Destroy(other.gameObject);

            // 나죽자
            Bullet bullet = other.GetComponent<Bullet>();
            Health -= bullet.Damage;

            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        // 플레이어와 충돌:
        if(other.CompareTag("Player"))
        {
            // 나죽자
            Destroy(this.gameObject);

            // 플레이어 체력이 0 이하일때만 죽인다.
            Player player = other.GetComponent<Player>(); // 게임 오브젝트의 컴포넌트를 가져온다.
            
            player.Health -= Damage;
            if(player.Health <= 0)
            {
                Destroy(player.gameObject);
            }
        }

    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 목표: 위로 계속 이동하고 싶다.

    // 필요 속성
    // - 이동 속도
    public float Speed = 5;

    // 기능
    // - 계속 위로 이동
    private void Move()
    {
        // 구현 순서
        // 1. 방향을 정한다.
        Vector2 dir = Vector2.up;

        // 2. 이동한다.
        transform.Translate(dir * Speed * Time.deltaTime);
    }


    void Update()
    {
        Move();
    }
}

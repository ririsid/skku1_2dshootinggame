using UnityEngine;

public enum BulletType
{
    Main,
    Sub
}

public class Bullet : MonoBehaviour
{
    // 목표: 위로 계속 이동하고 싶다.

    // 필요 속성
    // - 이동 속도
    public float Speed = 5;

    public BulletType BulletType;

    public int Damage;

    private void Start()
    {
        // 총알 타입에 따라 대미지를 다르게한다.
        switch(BulletType)
        {
            case BulletType.Main:
                Damage = 100;
                break;

            case BulletType.Sub:
                Damage = 60;
                break;
        }
    }


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

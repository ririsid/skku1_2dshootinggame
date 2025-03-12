using UnityEngine;

public class Player : MonoBehaviour
{
    // "응집도"는 높히고! "결합도"는 낮춰라
    // 응집도: 데이터와 데이터를 조작하는 로직이 한 곳에 모여 있는 구조 -> 응집도가 높은 구조
    // 결합도: 두 클래스간에 상호작용 의존 정도
    // 내 코드가 그렇게 이상한가요?(C#) -> 도메인 주도 설계 철저 입문(C#)
    
    public int Health = 100;

    public float Defence = 0.2f;
    
    public void TakeDamage(int damage)
    {
        Health -= (int)(damage * Defence);

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

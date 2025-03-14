using System;
using UnityEditorInternal;
using UnityEngine;


public enum ItemType
{
    HealthUp,
    AttackSpeedUp,
    MoveSpeedUp
}

public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;
    
    public float Value;

    // 둘 다 콜라이더가 무조건 있어야하고
    // 하나 이상이 리지드바디 있어야 한다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와만 충돌
        if (other.CompareTag("Player"))
        {
            // 효과 발동!
            Player player = other.GetComponent<Player>();

            switch (ItemType)
            {
                case ItemType.HealthUp:
                {
                    player.Health += (int)Value; // 프로퍼티, 메서드 처리
                    // 설계는 best가 없다. better가 있다.
                    break;
                }

                case ItemType.AttackSpeedUp:
                {
                    player.AttackCooltime += Value;
                    break;
                }

                case ItemType.MoveSpeedUp:
                {
                    player.MoveSpeed += Value;
                    break;
                }
            }
            
            Destroy(gameObject);
        }
    }
}

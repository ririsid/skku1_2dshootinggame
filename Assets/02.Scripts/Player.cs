using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 100;


    public void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

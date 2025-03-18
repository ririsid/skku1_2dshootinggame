using System;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float _timer = 0;
    private const float SHOW_TIME = 2f;
    
    public void Show()
    {
        gameObject.SetActive(true);

        _timer = 0f;
    }

    // 활성화 되어있을 경우 매 프레임마다 호출되는 이벤트 함수
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= SHOW_TIME)
        {
            Hide();
        }
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy == null) return;
            enemy.TakeDamage(100000);
        }
    }
}

using System;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float _timer = 0;
    private const float SHOW_TIME = 2f;

    private CameraShake _cameraShake;

    private void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _timer = 0f;

        _cameraShake.Shake();
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

            Damage damage = new Damage
            {
                Value = 100000,
                Type = DamageType.Boom,
            };

            enemy.TakeDamage(damage);
        }
    }
}

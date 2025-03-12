using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 목표: 일정 시간마다 적을 내 위치에 생성하고 싶다.
    
    // 필요 속성:
    // 일정 시간
    public float IntervalTime = 1f;
    // 현재 시간
    private float _currentTimer = 0f;
    
    // 적 프리팹
    public GameObject EnemyPrefab;


    private void Update()
    {
        // 1. 시간이 흐르다가
        _currentTimer += Time.deltaTime;
        
        // 2. 만약 스폰 타임이 된다면
        if (_currentTimer >= IntervalTime)
        {
            _currentTimer = 0f;
            
            // 3. 스폰을 한다.
            GameObject enemy = Instantiate(EnemyPrefab);
            enemy.transform.position = this.transform.position;
        }
        
        
    }
    
}

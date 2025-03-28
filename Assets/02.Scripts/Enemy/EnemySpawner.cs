using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 목표: 일정 시간마다 적을 내 위치에 생성하고 싶다.
    
    // 필요 속성:
    // 일정 시간
    public float IntervalTime = 1f;
    // 현재 시간
    private float _currentTimer = 0f;

    private void Update()
    {
        // 1. 시간이 흐르다가
        _currentTimer += Time.deltaTime;
        
        // 2. 만약 스폰 타임이 된다면
        if (_currentTimer >= IntervalTime)
        {
            _currentTimer = 0f;
            
            // IntervalTime = 0.6 ~ 1.5;
            IntervalTime = UnityEngine.Random.Range(0.6f, 1.5f);
            // 의사난수알고리즘
            // 1) 시드가 같아야한다.
            // 2) 알고리즘이 같아야한다.   Random.Range, C#, C++ STL 여러 가지 랜덤 엔진
            // 3) 호출 횟수가 같아야한다.
            
            // 3. 스폰을 한다.
            
            float percentage = Random.Range(0f, 1f);
            if (percentage <= 0.5f) // 50%
            {
                EnemyPool.Instance.Create(EnemyType.Basic, transform.position);
            }
            else if (percentage <= 0.8f)
            {
                EnemyPool.Instance.Create(EnemyType.Target, transform.position);
            }
            else
            {
                EnemyPool.Instance.Create(EnemyType.Follow, transform.position);
            }
        }
        
        
    }
    
}

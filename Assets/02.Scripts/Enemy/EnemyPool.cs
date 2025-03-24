using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // 오브젝트 풀 (적 창고)
    // ㄴ 게임 오브젝트(적 오브젝트)를 필요한 만큼 미리 생성해 두고 풀(창고)에 쌓아두는 기법이다.
    // ㄴ 오브젝트를 매번 생성하고 삭제하는 것보다 메모리 사용량과 성능 저하를 줄일 수 있다.
    // ㄴ 즉, 오버헤드를 줄인다.
    // ㄴ 컴퓨터 과학에 'Pool'은 사용할 때 획득한 메모리와 나중에 해제되는 메모리가 아닌
    //    미리 할당된 메모리 덩어리를 의미한다.
    
    // 목표: 적 풀에 적을 풀 사이즈 만큼 미리 생성해서 등록하고 싶다.
    // 속성
    // - 생성할 적 프리팹
    public List<Enemy> EnemyPrefabs;
  
    
    // - 풀 사이즈
    public int PoolSize = 20;
  
    // - 적을 관리할 풀 리스트
    private List<Enemy> _enemies;

    // 싱글톤
    public static EnemyPool Instance;
    
    // 기능
    // 1. 태어날 때
    private void Awake()
    {
        // ToDo: 하나임을 보장하는 코드로 변경
        Instance = this;
        
        // 2. 적 풀을 적을 담을 수 있는 크기로 만든다.
        int enemyPrefabCount = EnemyPrefabs.Count;
        _enemies = new List<Enemy>(PoolSize * enemyPrefabCount);
        
        // 3. 프리팹 갯수만큼 반복해서
        foreach (Enemy enemyPrefab in EnemyPrefabs)
        {
            // 4. 적 프리팹으로부터 적을 생성한다.
            for (int i = 0; i < PoolSize; ++i)
            {
                Enemy enemy = Instantiate(enemyPrefab);
                
                // 5. 생성할 적을 풀에 추가한다.
                _enemies.Add(enemy);
                
                // 적을 EnemyPool 폴더에 집어넣는다.
                // (하위 구조)
                enemy.transform.SetParent(this.transform);
                
                // 6. 비활성화 한다.
                enemy.gameObject.SetActive(false);
            }
        }
        
    }


    // 1. 응집도를 높혔다.
    // 2. 은닉화를 했다. (캡슐화)
    // 3. 객체 생성 로직을 분리했다. 
    // 싱글톤 + 오브젝트 풀링 + 팩토리 메서드 
    
    
    public Enemy Create(EnemyType EnemyType, Vector3 position)
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy.Data.EnemyType == EnemyType && enemy.gameObject.activeInHierarchy == false)
            {
                enemy.transform.position = position;
                        
                enemy.Initialize();
                        
                enemy.gameObject.SetActive(true);

                return enemy;
            }
        }

        return null;
    }
    
}

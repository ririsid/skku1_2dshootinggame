using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public List<Enemy> EnemyPrefabs;

    // - 풀 사이즈
    public int PoolSize = 20;

    public List<Enemy> Enemies;

    // 싱글톤
    public static EnemyPool Instance;

    // 기능
    // 1. 태어날 때
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 2. 총알 풀을 총알을 담을 수 있는 크기로 만든다.
        int enemyPrefabCount = EnemyPrefabs.Count;
        Enemies = new List<Enemy>(PoolSize * enemyPrefabCount);

        // 3. 풀 사이즈만큼 반복해서
        foreach (var enemyPrefab in EnemyPrefabs)
        {
            for (int i = 0; i < PoolSize; i++)
            {
                Enemy enemy = Instantiate(enemyPrefab);

                Enemies.Add(enemy);

                enemy.transform.SetParent(transform);

                enemy.gameObject.SetActive(false);
            }
        }
    }

    public Enemy Create(EnemyType enemyType, Vector3 position)
    {
        foreach (Enemy enemy in Enemies)
        {
            if (enemy.EnemyType == enemyType && enemy.gameObject.activeInHierarchy == false)
            {
                enemy.Initialize();

                enemy.transform.position = position;

                enemy.gameObject.SetActive(true);

                return enemy;
            }
        }

        return null;
    }
}

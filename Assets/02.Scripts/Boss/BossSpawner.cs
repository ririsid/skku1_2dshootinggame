using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public static BossSpawner Instance { get; private set; }

    public GameObject[] BossPrefabs;

    private GameObject _boss;

    public void Spawn()
    {
        if (_boss != null) return;
        CreateBoss();
        StopSpawnEnemies();
    }

    public void RemoveBoss()
    {
        if (_boss == null) return;
        Destroy(_boss);
        _boss = null;
        ResumeSpawnEnemies();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreateBoss()
    {
        _boss = Instantiate(BossPrefabs[0]);
        _boss.transform.position = transform.position;
    }

    private void StopSpawnEnemies()
    {
        // 모든 EnemySpawner Prefab을 찾아서 가져온다.
        var enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        // 가져온 EnemySpawner Prefab을 순회하면서 EnemySpawner Prefab의 스폰을 멈춘다.
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.StopSpawn();
        }
    }

    private void ResumeSpawnEnemies()
    {
        // 모든 EnemySpawner Prefab을 찾아서 가져온다.
        var enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        // 가져온 EnemySpawner Prefab을 순회하면서 EnemySpawner Prefab의 스폰을 재개한다.
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.StartSpawn();
        }
    }
}

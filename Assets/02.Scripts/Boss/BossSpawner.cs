using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public static BossSpawner Instance { get; private set; }

    public GameObject[] BossPrefabs;

    private GameObject _boss;

    public void Spawn()
    {
        StopSpawnEnemies();
        ShowWarningText();
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

    private void ShowWarningText()
    {
        UI_Game.Instance.ShowWarningText();
        // 2초 뒤에 CreateBoss 함수를 코루틴으로 호출한다.
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(2f); // 2초 대기
        if (_boss != null) yield break;
        CreateBoss();
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

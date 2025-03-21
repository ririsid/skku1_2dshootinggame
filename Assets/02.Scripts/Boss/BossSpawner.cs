using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public static BossSpawner Instance { get; private set; }

    public GameObject[] BossPrefabs;

    private GameObject _boss;

    private GameObject _player;

    public void Spawn()
    {
        StopSpawnEnemies();
        ShowWarningText();
    }

    public void RemoveBoss()
    {
        if (_boss == null) return;
        UI_Game.Instance.HideBossHealthSlider();
        Destroy(_boss);
        _boss = null;
        StartCoroutine(ResumeSpawnEnemiesCoroutine());
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
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        // HoldPlayer();
        yield return new WaitForSeconds(2f); // 2초 대기
        if (_boss != null) yield break;
        CreateBoss();
        // ResumePlayer();
    }

    private void HoldPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null) return;
        _player.GetComponent<Player>().HoldFire();
    }

    private void ResumePlayer()
    {
        if (_player == null) return;
        _player.GetComponent<Player>().ResumeFire();
    }

    private void CreateBoss()
    {
        _boss = Instantiate(BossPrefabs[0]);
        _boss.transform.position = transform.position;
        var boss = _boss.GetComponent<Boss>();
        UI_Game.Instance.SetBossHealthSlider(boss.Health);
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

    private IEnumerator ResumeSpawnEnemiesCoroutine()
    {
        yield return new WaitForSeconds(2f);
        ResumeSpawnEnemies();
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

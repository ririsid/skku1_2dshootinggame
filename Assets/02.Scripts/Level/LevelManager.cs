using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private List<LevelDataSO> _levelData;

    public Player MyPlayer;

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
    }

    public LevelDataSO GetLevelData()
    {
        int score = MyPlayer.PlayerData.Score;
        foreach (var levelData in _levelData)
        {
            if (score < levelData.Score)
            {
                return levelData;
            }
        }
        return _levelData[^1];
    }
}

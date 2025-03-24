using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private List<LevelDataSO> _levelDatas;

    public Player MyPlayer;
    
    private void Awake()
    {
        Instance = this;
    }

    public LevelDataSO GetLevelData()
    {
        int score = MyPlayer.PlayerData.Score;

        foreach (LevelDataSO levelData in _levelDatas)
        {
            if (score < levelData.Score)
            {
                return levelData;
            }
        }

        return _levelDatas[^1]; // 더이상 레벨이 없다면 가장 마지막 레벨 반환
        // == return LevelDatas[LevelDatas.Count - 1]; 
    }
}

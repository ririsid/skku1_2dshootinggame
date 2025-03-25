using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    // 관리자: 추가, 삭제, 조회, 정렬, 
    // 레벨업, 조회
    public static StatManager Instance;

    // 데이터
    public List<StatDataSO> StatDataList;
    

    private List<Stat> _stats = new List<Stat>();
    public List<Stat> Stats => _stats;

    
    public List<UI_StatButton> UI_StatButtons;
    
    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            _stats.Add(new Stat((StatType)i, 1, StatDataList[i]));
        }
        
        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            UI_StatButtons[i]._stat = _stats[i];
        }
    }

    private void Start()
    {
        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            UI_StatButtons[i].Refresh();
        }
    }

    public bool TryLevelUp(StatType statType)
    {
        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            UI_StatButtons[i].Refresh();
        }
        
        return _stats[(int)statType].TryUpgrade();
    }
}

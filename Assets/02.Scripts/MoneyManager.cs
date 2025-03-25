using System.Collections.Generic;
using UnityEngine;

public enum MoneyType
{
    Gold,
    Diamond,
    Ruby
}

public class MoneyManager : MonoBehaviour
{
    // Manager: '재화'관리자
    //  ㄴ 싱글톤(Instance라는 정적필드로 쉽게 접근이 가능하다.)
    //  ㄴ '재화' 관리: 1.재화 추가 * 재화 종류,
    //                2.재화 조회 * 재화 종류,
    //                3.재화 소비 * 재화 종류
    
    public static MoneyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<int> moneyList = new List<int>(new int[3]); // 0: 골드
                                                                    // 1: 다이아몬드
                                                                    // 2: 루비
    public int Gold    => moneyList[(int)MoneyType.Gold];
    
    public int Diamond => moneyList[(int)MoneyType.Diamond];
    
    public int Ruby    => moneyList[(int)MoneyType.Ruby];

    // 추가 메서드
    public void Add(MoneyType moneyType, int amount)
    {
        moneyList[(int)moneyType] += amount;
    }
    
    // 검색 메서드
    public bool Have(MoneyType moneyType,int amount)
    {
        return moneyList[(int)moneyType] >= amount;
    }
    
    // 삭제(소비) 메서드
    public bool TryConsume(MoneyType moneyType, int amount)
    {
        if (!Have(moneyType, amount))
        {
            return false;
        }
        
        moneyList[(int)moneyType] -= amount;
        return true;
    }
}

using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gold,      
    Diamond, 
    
    Count    
}

// 재화 관리자
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private List<int> _values = new List<int>(new int[(int)CurrencyType.Count]);
    
    private void Awake()
    {
        Instance = this;

        Load();
    }

    public int Gold    => _values[(int)CurrencyType.Gold];
    public int Diamond => _values[(int)CurrencyType.Diamond];
    
    // 재화량
    public int Get(CurrencyType currencyType)
    {
        return _values[(int)currencyType];
    }
    
    // 재화 추가
    public void Add(CurrencyType currencyType, int amount)
    {
        _values[(int)currencyType] += amount;
        
        Save();
    }

    // 재화 가지고 있니?
    public bool Have(CurrencyType currencyType, int amount)
    {
        return _values[(int)currencyType] >= amount;
    }
    
    // 재화 소모
    public bool TryConsume(CurrencyType currencyType, int amount)
    {
        if (!Have(currencyType, amount))
        {
            return false;
        }
        
        _values[(int)currencyType] -= amount;

        Save();
        
        return true;
    }

    private const string SAVE_KEY = "Currency";
    
    private void Save()
    {
        string jsonData = JsonUtility.ToJson(_values);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string jsonData = PlayerPrefs.GetString(SAVE_KEY);
            _values = JsonUtility.FromJson<List<int>>(jsonData);
        }
    }
}

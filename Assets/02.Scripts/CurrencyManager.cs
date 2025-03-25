using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gold,
    Diamond,

    Count
}

public class CurrencySaveData
{
    public List<int> Values = new List<int>(new int[(int)CurrencyType.Count]);
}

public class CurrencyManager : MonoBehaviour
{
    private const string SAVE_KEY = "Currency";

    public static CurrencyManager Instance = null;

    public delegate void OnDataChanged();
    public OnDataChanged OnDataChangedCallback = null;

    private CurrencySaveData _saveData;

    private List<int> _values => _saveData.Values;

    public int Get(CurrencyType currencyType)
    {
        return _values[(int)currencyType];
    }

    public void Add(CurrencyType currencyType, int amount)
    {
        _values[(int)currencyType] += amount;
        Save();
    }

    public bool Have(CurrencyType currencyType, int amount)
    {
        return _values[(int)currencyType] >= amount;
    }

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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Load();
    }

    private void Save()
    {
        if (_saveData == null)
        {
            _saveData = new CurrencySaveData();
        }
        OnDataChangedCallback?.Invoke();
        string jsonData = JsonUtility.ToJson(_saveData);
        SecurePlayerPrefs.SetString(SAVE_KEY, jsonData);
    }

    private void Load()
    {
        if (SecurePlayerPrefs.HasKey(SAVE_KEY))
        {
            string jsonData = SecurePlayerPrefs.GetString(SAVE_KEY);
            _saveData = JsonUtility.FromJson<CurrencySaveData>(jsonData);
        }
        else
        {
            _saveData = new CurrencySaveData();
        }
    }
}

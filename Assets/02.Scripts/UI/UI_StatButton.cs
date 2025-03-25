using TMPro;
using UnityEngine;

public class UI_StatButton : MonoBehaviour
{
    public Stat _stat;

    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI ValueTextUI;
    public TextMeshProUGUI CostTextUI;


    public void Refresh()
    {
        NameTextUI.text = _stat.StatType.ToString();
        ValueTextUI.text = $"{_stat.Value}";
        CostTextUI.text = $"{_stat.Cost:N0}";

        if (CurrencyManager.Instance.Have(CurrencyType.Gold, _stat.Cost))
        {
            CostTextUI.color = Color.black;
        }
        else
        {
            CostTextUI.color = Color.red;
        }
    }

    public void OnClickLevelUp()
    {
        if (StatManager.Instance.TryLevelUp(_stat.StatType))
        {
            Debug.Log($"{_stat.StatType} 레벨업!");
            // 업그레이드 성공 이펙트 실행            
        }
        else
        {
            Debug.Log($"돈이 부족합니다!");
            // 돈이 부족합니다 토스팝업 Show
        }
    }

}

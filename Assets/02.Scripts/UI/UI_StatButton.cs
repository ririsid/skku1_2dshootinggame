using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatButton : MonoBehaviour
{
    public Stat _stat;

    // public TextMeshProUGUI NameTextUI;
    // public TextMeshProUGUI ValueTextUI;
    public TextMeshProUGUI CostTextUI;
    public Image IconImageUI;


    public void Refresh()
    {
        // NameTextUI.text = _stat.StatType.ToString();
        // ValueTextUI.text = $"{_stat.Value}";
        CostTextUI.text = $"{_stat.Cost:N0}";

        if (CurrencyManager.Instance.Have(CurrencyType.Gold, _stat.Cost))
        {
            CostTextUI.color = Color.white;
            IconImageUI.color = Color.white;
        }
        else
        {
            CostTextUI.color = Color.black;
            IconImageUI.color = Color.black;
        }
    }

    public void OnClickLevelUp()
    {
        if (StatManager.Instance.TryLevelUp(_stat.StatType))
        {
            Debug.Log($"{_stat.StatType} 레벨업!");
            // 업그레이드 성공 이펙트 실행
            transform.DOScale(1.2f, 0.1f).OnComplete(() => transform.DOScale(1f, 0.1f));
        }
        else
        {
            Debug.Log($"돈이 부족합니다!");
            // 돈이 부족합니다 토스팝업 Show
        }
    }

}

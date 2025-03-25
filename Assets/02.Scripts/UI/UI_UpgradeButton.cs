using TMPro;
using UnityEngine;

public struct UI_UpgradeButtonData
{
    public string Name;
    public int Value;
    public int Cost;
}

public class UI_UpgradeButton : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    public TextMeshProUGUI ValueText;

    public TextMeshProUGUI CostText;

    public string Name => NameText.text;
    public int Value
    {
        get
        {
            return int.Parse(ValueText.text);
        }
        set
        {
            ValueText.text = value.ToString();
        }
    }
    public int Cost
    {
        get
        {
            return int.Parse(CostText.text);
        }
        set
        {
            CostText.text = value.ToString();
        }
    }
}

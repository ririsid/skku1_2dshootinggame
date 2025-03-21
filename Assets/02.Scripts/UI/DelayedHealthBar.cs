using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DelayedHealthBar : MonoBehaviour
{
    public Slider BackSlider;
    public Slider FrontSlider;

    public float MaxHealth
    {
        set
        {
            BackSlider.maxValue = value;
            BackSlider.value = 0f;
            FrontSlider.maxValue = value;
            FrontSlider.value = 0f;
            FrontSlider.DOValue(value, 2f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                BackSlider.value = value;
            });
        }
    }

    public float CurrentHealth
    {
        set
        {
            BackSlider.DOValue(value, 1f).SetEase(Ease.OutCubic);
            FrontSlider.value = value;
        }
    }
}

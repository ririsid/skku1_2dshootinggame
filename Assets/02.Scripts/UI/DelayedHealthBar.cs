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
            FrontSlider.maxValue = value;
            InitializeHealthBar(value);
        }
    }

    public float CurrentHealth
    {
        set
        {
            FrontSlider.value = value;
            DelayedSetValues(value);
        }
    }

    private void InitializeHealthBar(float value)
    {
        BackSlider.value = 0f;
        FrontSlider.value = 0f;
        FrontSlider.DOValue(value, 2f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            BackSlider.value = value;
        });
    }

    private void DelayedSetValues(float value)
    {
        BackSlider.DOValue(value, 1f).SetEase(Ease.OutCubic);
    }
}

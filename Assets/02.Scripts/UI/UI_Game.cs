using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    public static UI_Game Instance = null;

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
    }

    // 목적: 필살기 갯수, 킬 카운드 등 UI를 담당하고 싶다.
    // 속성:
    // - 필살기 갯수 UI
    public List<GameObject> Booms;

    // - 킬 카운트 UI
    public TextMeshProUGUI KillText;

    // - 점수 UI
    public TextMeshProUGUI ScoreText;

    // - 경고 텍스트 UI
    public TextMeshProUGUI WarningText;

    // - 보스 체력 슬라이더
    public Slider BossHealthSlider;

    // 기능: 새로고침
    public void Refresh(int boomCount, int killCount)
    {
        // 필살기 갯수에 따라 필살기 UI들을 켜고 끈다.
        for (int i = 0; i < 3; i++)
        {
            Booms[i].SetActive(i < boomCount);
        }

        // 킬 횟수 텍스트 새로고침
        KillText.text = $"Kills: {killCount}";
    }

    public void RefreshScore(int score)
    {
        // 문자열 포멧
        ScoreText.text = score.ToString("N0");
        ScoreText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f)
            .OnComplete(() =>
        {
            // 람다식
            ScoreText.rectTransform.localScale = Vector3.one;
        });
    }

    public void ShowWarningText()
    {
        WarningText.rectTransform.anchoredPosition = new Vector2(0, 700);
        WarningText.gameObject.SetActive(true);
        // 경고 텍스트를 2번 반복하면서 페이드 인/아웃하고,
        WarningText.rectTransform.DOScale(1.2f, 0.5f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            WarningText.rectTransform.DOAnchorPosY(WarningText.rectTransform.anchoredPosition.y + 400, 0.1f)
            .OnComplete(() => WarningText.gameObject.SetActive(false));
        });
    }

    public void SetBossHealthSlider(int maxHealth)
    {
        BossHealthSlider.gameObject.SetActive(true);
        BossHealthSlider.maxValue = maxHealth;
        BossHealthSlider.value = 0;
        BossHealthSlider.DOValue(maxHealth, 2f).SetEase(Ease.OutCubic);
    }

    public void UpdateBossHealthSlider(int currentHealth)
    {
        BossHealthSlider.value = currentHealth;
    }

    public void HideBossHealthSlider()
    {
        BossHealthSlider.gameObject.SetActive(false);
    }
}

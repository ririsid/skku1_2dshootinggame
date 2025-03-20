using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

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
        DOTween.KillAll(true);
        ScoreText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f)
            .OnComplete(() =>
        {
            // 람다식
            ScoreText.rectTransform.localScale = Vector3.one;
        });
    }
}

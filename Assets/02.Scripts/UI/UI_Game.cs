using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UI_Game : MonoBehaviour
{
    // 목적: 필살기 갯수, 킬 카운드 등 UI를 담당하고 싶다.
    // 속성:
    // - 필살기 갯수 UI
    public List<GameObject> Booms;

    // - 킬 카운트 UI
    public TextMeshProUGUI KillText;

    // - 점수 UI
    public TextMeshProUGUI ScoreText;

    private int _score = 0;

    // 기능: 새로고침
    public void Refresh(int boomCount, int killCount, int score)
    {
        // 필살기 갯수에 따라 필살기 UI들을 켜고 끈다.
        for (int i = 0; i < 3; i++)
        {
            Booms[i].SetActive(i < boomCount);
        }

        // 킬 횟수 텍스트 새로고침
        KillText.text = string.Format("Kills: {0:N0}", killCount);

        // 점수 텍스트 새로고침
        ChangeScoreWithScaleEffect(score);
    }

    public void ChangeScoreWithScaleEffect(int score)
    {
        bool isChanged = _score != score;
        ScoreText.text = string.Format("Score: {0:N0}", score);
        _score = score;

        if (!isChanged) return;
        // 스케일 초기화
        ScoreText.transform.localScale = Vector3.one;

        // 스케일 이펙트 적용
        Sequence sequence = DOTween.Sequence();
        sequence.Append(ScoreText.transform.DOScale(1.2f, 0.2f));
        sequence.Append(ScoreText.transform.DOScale(1f, 0.1f));
    }

}

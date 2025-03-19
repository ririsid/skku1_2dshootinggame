using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro; // 텍스트 메시 프로는 현재 유니티에서 기본으로 쓰고있는 텍스트 컴포넌트



public class UI_Game : MonoBehaviour
{
    // 목적: 필살기 개수, 킬 카운트 등 UI를 담당하고 싶다.
    // 속성:
    // - 필살기 개수 UI
    public List<GameObject> Booms;
    
    // - 킬 카운트 UI
    public TextMeshProUGUI KillText;

    public TextMeshProUGUI ScoreText;


    // 스마트 UI -> UI는 말 그대로 사용자와 게임간의 인터랙티브한 요소를 보여주는 책임만 가져야지
    //          -> 그 외 데이터와 데이터를 다루는 로직이 들어가면 책임이 커져서 UI 범위를 벗어난다.
    
    
    // 기능: 새로고침
    public void Refresh(int boomCount, int killCount)
    {
        // 필살기 개수에 따라 필살기 ui들을 키고 끈다.
        for (int i = 0; i < 3; ++i)
        {
            Booms[i].SetActive(i < boomCount);
        }

        // 킬 횟수 텍스트 새로고침
        KillText.text = $"Kills: {killCount}";
    }

    public void RefreshScore(int score)
    { 
        // 문자열 포맷
        ScoreText.text = score.ToString("N0");
        ScoreText.rectTransform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.2f)
            .SetEase(Ease.OutBounce)
            //.SetLoops(-1, LoopType.Yoyo)
            .OnComplete(() =>
        {
            // 람다식
            ScoreText.rectTransform.localScale = Vector3.one;
        });

        // 구현 방법:
        // 애니메이션
        // 딜레이와 +보간 주는 로직
        // dotween 사용
    }
}

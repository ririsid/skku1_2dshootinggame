using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro; // 텍스트 메시 프로는 현재 유니티에서 기본으로 쓰고있는 텍스트 컴포넌트



// 특징
// UI_Game을 참조하는 코드가 점점 많아져서 귀찮다.
// ㄴ 이것을 어떻게.. 한방에 쉽게 접근할 수 없을까?
// ㄴ UI_Game의 인스턴스는 게임(프로그램) 내에 딱 하나만 존재하는구나...

// 싱글톤 패턴
// - 인스턴스가 단 하나임을 보장한다.
// - 전역적인 접근이 가능하다.
// 게임 개발에서는 관리자(Manager)와 UI(하나만 필요한) 클래스를 싱글톤 패턴으로 설계하는 것이 일반적인 관행이다.
// (인스턴스가 단 하나임을 보장하고, 쉽게 접근할 수 있으므로 여러 가지 장점이 있다.)
// - 전역 접근 , 코드 단순화(해당 관리자를 찾기 위한 복잡한 로직이 필요없다.)
// - 메모미 및 리소스 관리가 용이하다. (중복 생성 X)

public class UI_Game : MonoBehaviour
{
    public static UI_Game Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("싱글톤은 인스턴스가 단 하나임을 보장한다!!! (문의사항이 있을 경우 차수민에게 찾아오세요.)");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    
    // 목적: 필살기 개수, 킬 카운트 등 UI를 담당하고 싶다.
    // 속성:
    // - 필살기 개수 UI
    public List<GameObject> Booms;
    
    // - 킬 카운트 UI
    public TextMeshProUGUI KillText;

    public TextMeshProUGUI ScoreText;
   
    public TextMeshProUGUI GoldText;


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
        DOTween.KillAll(true);
        ScoreText.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f)
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

    public void RefreshGold(int gold)
    {
        GoldText.text = gold.ToString("N0");
    }
}

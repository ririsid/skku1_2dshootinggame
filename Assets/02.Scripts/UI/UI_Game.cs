using UnityEngine;
using System.Collections.Generic;
using TMPro; // 텍스트 메시 프로는 현재 유니티에서 기본으로 쓰고있는 텍스트 컴포넌트



public class UI_Game : MonoBehaviour
{
    // 목적: 필살기 개수, 킬 카운트 등 UI를 담당하고 싶다.
    // 속성:
    // - 필살기 개수 UI
    public List<GameObject> Booms;
    
    // - 킬 카운트 UI
    public TextMeshProUGUI KillText;

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
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        ScoreText.text = string.Format("Score: {0:N0}", score);
    }
}

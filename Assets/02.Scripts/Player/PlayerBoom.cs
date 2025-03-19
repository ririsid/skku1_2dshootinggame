using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBoom : MonoBehaviour
{
    public Boom _boom;

    public UI_Game GameUI;

    private const int MAX_COUNT = 3;
    private const int ADD_COUNT = 20;

    private int _boomCount = 0;

    private int _killCount = 0;

    private int _score = 0;

    public void AddKillCount()
    {
        _killCount++;
        if (_killCount >= ADD_COUNT)
        {
            _killCount = 0;
            _boomCount = Math.Min(_boomCount + 1, MAX_COUNT);
        }
    }

    public void AddScore(int score)
    {
        _score += score;

        // 게임 UI를 새로고침 한다.
        GameUI.Refresh(_boomCount, _killCount, _score);
    }

    private void Start()
    {
        // 게임 UI를 새로고침 한다.
        GameUI.Refresh(_boomCount, _killCount, _score);
    }

    private void Update()
    {
        if (_boomCount <= 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _boomCount -= 1;
            _boom.Show();

            // 게임 UI를 새로고침 한다.
            GameUI.Refresh(_boomCount, _killCount, _score);
        }
    }
}

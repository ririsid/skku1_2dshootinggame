using System;
using UnityEngine;

public class PlayerBoom : PlayerComponent
{
    public Boom _boom;

    public UI_Game GameUI;
    
    private const int MAX_COUNT = 3;
    private const int ADD_COUNT = 20;
    

    private void Start()
    {
        // 게임 UI를 새로고침 한다.
        GameUI.Refresh(_player.PlayerData.BoomCount, _player.PlayerData.KillCount);
    }

    public void AddKillCount()
    {
        _player.PlayerData.KillCount++;
        if (_player.PlayerData.KillCount >= ADD_COUNT)
        {
            _player.PlayerData.KillCount = 0;
            _player.PlayerData.BoomCount = Math.Min(_player.PlayerData.BoomCount + 1, MAX_COUNT);
        }
        
        // 게임 UI를 새로고침 한다.
        GameUI.Refresh(_player.PlayerData.BoomCount, _player.PlayerData.KillCount);
    }
    
    private void Update()
    {
       
        if (_player.PlayerData.BoomCount <= 0)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _player.PlayerData.BoomCount -= 1;
            _boom.Show();
        }
    }
}

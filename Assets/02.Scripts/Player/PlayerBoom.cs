using System;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    public Boom _boom;
    
    private const int MAX_COUNT = 3;
    private const int ADD_COUNT = 20;

    private int _boomCount = 0;

    private int _killCount = 0;

    public void AddKillCount()
    {
        _killCount++;
        if (_killCount >= ADD_COUNT)
        {
            _boomCount = Math.Min(_boomCount + 1, MAX_COUNT);
        }
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
        }
    }
}

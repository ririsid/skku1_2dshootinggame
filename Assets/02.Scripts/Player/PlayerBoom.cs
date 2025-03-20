using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBoom : PlayerComponent
{
    public Boom _boom;

    private void Update()
    {

        if (_player.HasBoom == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _player.SubstractBoomCount();
            _boom.Show();
        }
    }
}

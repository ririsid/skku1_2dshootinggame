using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBoom : MonoBehaviour
{
    public Boom _boom;

    private Player _player;

    public UI_Game GameUI;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

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

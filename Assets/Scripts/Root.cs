using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameSettings _settings;
    private LogicStartup _startup;
    
    private void Awake()
    {
        _startup = new LogicStartup(new LogicStartup.Ctx
        {
            playerTransform = _player,
            joystickInput = _joystick,
            settings = _settings
        });
    }

    private void Update()
    {
        _startup.Run();
    }

    private void OnDestroy()
    {
        _startup.Destroy();
    }
}

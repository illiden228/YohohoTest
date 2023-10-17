using Core;
using DefaultNamespace;
using GameLogic.Systmes;
using Joystick_Pack.Scripts.Joysticks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class LogicStartup : BaseDisposable
{
    public struct Ctx
    {
        public Transform playerTransform;
        public IJoystickInput joystickInput;
        public GameSettings settings;
    }

    private readonly Ctx _ctx;
    private EcsWorld _world;
    private EcsSystems _systems;

    public LogicStartup(Ctx ctx)
    {
        _ctx = ctx;
            
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
            
        _systems
            .Add(new InitPlayerSystem())
            .Add(new PlayerMoveSystem())
            .Add(new PlayerViewSystem())
            .Inject()
            .Inject(_ctx.joystickInput)
            .Inject(_ctx.settings)
            .Inject(_ctx.playerTransform)
            .Init();
    }

    public void Run()
    {
        _systems.Run();
    }

    public void Destroy()
    {
        _systems?.Destroy();
        _world?.Destroy();
    }
}
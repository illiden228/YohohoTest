using Core;
using DefaultNamespace;
using GameLogic;
using GameView;
using GameView.View;
using Joystick_Pack.Scripts.Joysticks;
using Leopotam.EcsLite;
using UnityEngine;

public class EcsStartup : BaseDisposable, IEcsStartup
{
    public struct Ctx
    {
        public Transform playerTransform;
        public IJoystickInput joystickInput;
        public GameSettings settings;
        public GameStartData gameStartData;
        public ItemSpawnerView spawnerView;
        public ItemRecieverView recieverView;
        public PlayerItemView playerView;
    }

    private readonly Ctx _ctx;
    private EcsWorld _world;
    private LogicStartup _logicStartup;
    private ViewStartup _viewStartup;

    public EcsStartup(Ctx ctx)
    {
        _ctx = ctx;
            
        _world = new EcsWorld();
        
        
        _logicStartup = new LogicStartup(new LogicStartup.Ctx
        {
            joystickInput = _ctx.joystickInput,
            settings = _ctx.settings,
            world = _world,
            gameStartData = _ctx.gameStartData
        });

        _viewStartup = new ViewStartup(new ViewStartup.Ctx
        {
            playerTransform = _ctx.playerTransform,
            world = _world,
            spawnerView = _ctx.spawnerView,
            recieverView = _ctx.recieverView,
            playerView = _ctx.playerView
        });
    }


    public void Run()
    {
        _logicStartup.Run();
        _viewStartup.Run();
    }

    public void Destroy()
    {
        _logicStartup.Destroy();
        _viewStartup.Destroy();
        _world?.Destroy();
    }
}
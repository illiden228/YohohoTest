using Core;
using DefaultNamespace;
using GameLogic.Systmes;
using Joystick_Pack.Scripts.Joysticks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic
{
    public class LogicStartup : BaseDisposable, IEcsStartup
    {
        public struct Ctx
        {
            public IJoystickInput joystickInput;
            public GameSettings settings;
            public EcsWorld world;
            public GameStartData gameStartData;
        }

        private readonly Ctx _ctx;
        private EcsSystems _systems;

        public LogicStartup(Ctx ctx)
        {
            _ctx = ctx;
            
            _systems = new EcsSystems(_ctx.world);
            
            _systems
                .Add(new InitPlayerSystem())
                .Add(new InitItemSpawnerSystem())
                .Add(new InitItemRecieverSystem())
                .Add(new PlayerMoveSystem())
                .Add(new SpawnCooldownSystem())
                .Add(new ItemSpawnSystem())
                .Add(new InteractSystem())
                .Add(new InterractItemSpawnerSystem())
                .Add(new InterractItemRecieverSystem())
                .Inject()
                .Inject(_ctx.joystickInput)
                .Inject(_ctx.settings)
                .Inject(_ctx.gameStartData)
                .Init();
        }

        public void Run()
        {
            _systems.Run();
        }

        public void Destroy()
        {
            _systems?.Destroy();
        }
    }
}
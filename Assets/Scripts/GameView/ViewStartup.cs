using Core;
using DefaultNamespace;
using GameView.Systmes;
using GameView.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameView
{
    public class ViewStartup : BaseDisposable, IEcsStartup
    {
        public struct Ctx
        {
            public Transform playerTransform;
            public EcsWorld world;
            public ItemSpawnerView spawnerView;
            public ItemRecieverView recieverView;
            public PlayerItemView playerView;
        }

        private readonly Ctx _ctx;
        private EcsSystems _systems;

        public ViewStartup(Ctx ctx)
        {
            _ctx = ctx;
            
            _systems = new EcsSystems(_ctx.world);
            
            _systems
                .Add(new PlayerViewSystem())
                .Add(new ItemSpawnerViewSystem())
                .Add(new ItemRecieverViewSystem())
                .Add(new PlayerItemsViewSystem())
                .Inject()
                .Inject(_ctx.playerTransform)
                .Inject(_ctx.spawnerView)
                .Inject(_ctx.recieverView)
                .Inject(_ctx.playerView)
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
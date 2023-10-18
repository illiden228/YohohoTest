using System.Collections.Generic;
using DefaultNamespace;
using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic.Systmes
{
    public class InitPlayerSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<PlayerTag> _playerPool = default;
        private readonly EcsPoolInject<PositionComponent> _positionPool = default;
        private readonly EcsPoolInject<BagComponent> _bagPool = default;
        private readonly EcsCustomInject<GameStartData> _startData = default;
        private readonly EcsCustomInject<GameSettings> _settings = default;
        
        public void Init(IEcsSystems systems)
        {
            var playerEntity = _world.Value.NewEntity();
            _playerPool.Value.Add(playerEntity);
            _positionPool.Value.Add(playerEntity).position = _startData.Value.StartPosition;

            ref var bagComponent = ref _bagPool.Value.Add(playerEntity);
            bagComponent.items = new Stack<EcsPackedEntity>();
            bagComponent.maxCount = _settings.Value.PlayerStackMaxCount;
        }
    }
}
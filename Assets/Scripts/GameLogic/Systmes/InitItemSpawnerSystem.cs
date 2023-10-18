using System.Collections.Generic;
using DefaultNamespace;
using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic.Systmes
{
    public class InitItemSpawnerSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<ItemSpawnerTag> _itemSpawnerPool = default;
        private readonly EcsPoolInject<BagComponent> _bagPool = default;
        private readonly EcsPoolInject<CooldownTimeComponent> _cooldownPool = default;
        private readonly EcsPoolInject<PositionComponent> _positionPool = default;
        private readonly EcsPoolInject<InteractComponent> _interactPool = default;
        private readonly EcsCustomInject<GameStartData> _startData;
        private readonly EcsCustomInject<GameSettings> _settings;
        
        public void Init(IEcsSystems systems)
        {
            var itemSpawner = _world.Value.NewEntity();
            _itemSpawnerPool.Value.Add(itemSpawner);
            
            ref var bagComponent = ref _bagPool.Value.Add(itemSpawner);
            bagComponent.items = new Stack<EcsPackedEntity>();
            bagComponent.maxCount = _settings.Value.ItemSpawnerStackMaxCount;
            
            _cooldownPool.Value.Add(itemSpawner).MaxTime = _settings.Value.ItemSpawnCooldown;

            _positionPool.Value.Add(itemSpawner).position = _startData.Value.StartItemSpawnerPosition;
            
            _interactPool.Value.Add(itemSpawner).InteractRadius = _settings.Value.InteractRadius;
        }
    }
}
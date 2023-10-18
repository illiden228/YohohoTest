using System.Collections.Generic;
using DefaultNamespace;
using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic.Systmes
{
    public class InitItemRecieverSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<ItemRecieverTag> _itemRecieverPool = default;
        private readonly EcsPoolInject<BagComponent> _bagPool = default;
        private readonly EcsPoolInject<PositionComponent> _positionPool = default;
        private readonly EcsPoolInject<InteractComponent> _interactPool = default;
        private readonly EcsCustomInject<GameStartData> _startData;
        private readonly EcsCustomInject<GameSettings> _settings;
        
        public void Init(IEcsSystems systems)
        {
            var itemReciever = _world.Value.NewEntity();
            _itemRecieverPool.Value.Add(itemReciever);
            
            ref var bagComponent = ref _bagPool.Value.Add(itemReciever);
            bagComponent.items = new Stack<EcsPackedEntity>();
            bagComponent.maxCount = _settings.Value.ItemRecieverStackMaxCount;

            _positionPool.Value.Add(itemReciever).position = _startData.Value.StartItemRecieverPosition;

            _interactPool.Value.Add(itemReciever).InteractRadius = _settings.Value.InteractRadius;
        }
    }
}
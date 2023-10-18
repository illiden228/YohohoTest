using GameLogic.Components;
using GameView.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameView.Systmes
{
    public class ItemSpawnerViewSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<ItemSpawnerView> _itemSpawnerView = default;
        private readonly EcsFilterInject<Inc<BagComponent, ItemSpawnerTag>> _filter = default;
        private readonly EcsPoolInject<BagComponent> _bagPool = default;
        private int _lastCount = -1;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var spawnerEntity in _filter.Value)
            {
                ref var bag = ref _bagPool.Value.Get(spawnerEntity);
                if(bag.items.Count == _lastCount)
                    continue;
                
                _itemSpawnerView.Value.SetCount(bag.items.Count, bag.maxCount);
                _lastCount = bag.items.Count;
            }
        }
    }
}
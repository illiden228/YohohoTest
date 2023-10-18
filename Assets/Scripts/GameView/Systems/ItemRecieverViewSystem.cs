using GameLogic.Components;
using GameView.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameView.Systmes
{
    public class ItemRecieverViewSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<ItemRecieverView> _recieverSpawnerView = default;
        private readonly EcsFilterInject<Inc<BagComponent, ItemRecieverTag>> _filter = default;
        private readonly EcsPoolInject<BagComponent> _bagPool = default;
        private int _lastCount = -1;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var recieverEntity in _filter.Value)
            {
                ref var bag = ref _bagPool.Value.Get(recieverEntity);
                if(bag.items.Count == _lastCount)
                    continue;
                
                _recieverSpawnerView.Value.SetCount(bag.items.Count, bag.maxCount);
                _lastCount = bag.items.Count;
            }
        }
    }
}
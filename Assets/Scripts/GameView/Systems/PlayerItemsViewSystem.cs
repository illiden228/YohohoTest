using GameLogic.Components;
using GameView.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameView.Systmes
{
    public class PlayerItemsViewSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<PlayerItemView> _itemPlayerView = default;
        private readonly EcsFilterInject<Inc<BagComponent, PlayerTag>> _filter = default;
        private readonly EcsPoolInject<BagComponent> _bagPool = default;
        private int _lastCount = -1;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _filter.Value)
            {
                ref var bag = ref _bagPool.Value.Get(playerEntity);
                if(bag.items.Count == _lastCount)
                    continue;
                
                _itemPlayerView.Value.SetCount(bag.items.Count, bag.maxCount);
                _lastCount = bag.items.Count;
            }
        }
    }
}
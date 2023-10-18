using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic.Systmes
{
    public class InterractItemRecieverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InteractEvent, ItemRecieverTag>> _interractFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTag>> _playerFilter = default;
        private readonly EcsPoolInject<BagComponent> _bagPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var recieverEntity in _interractFilter.Value)
            {
                foreach (var playerEntity in _playerFilter.Value)
                {
                    ref var recieverBag = ref _bagPool.Value.Get(recieverEntity);
                    ref var playerBag = ref _bagPool.Value.Get(playerEntity);

                    if(playerBag.items.Count == 0)
                        continue;
                    if (recieverBag.maxCount <= recieverBag.items.Count)
                        continue;
                    
                    recieverBag.items.Push(playerBag.items.Pop());
                }
            }
        }
    }
}
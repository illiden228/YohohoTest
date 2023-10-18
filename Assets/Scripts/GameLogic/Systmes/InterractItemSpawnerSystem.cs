using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic.Systmes
{
    public class InterractItemSpawnerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InteractEvent, ItemSpawnerTag>> _interractFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTag>> _playerFilter = default;
        private readonly EcsPoolInject<BagComponent> _bagPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var spawnerEntity in _interractFilter.Value)
            {
                foreach (var playerEntity in _playerFilter.Value)
                {
                    ref var spawnerBag = ref _bagPool.Value.Get(spawnerEntity);
                    ref var playerBag = ref _bagPool.Value.Get(playerEntity);

                    if(spawnerBag.items.Count == 0)
                        continue;
                    if (playerBag.maxCount <= playerBag.items.Count)
                        continue;
                    
                    playerBag.items.Push(spawnerBag.items.Pop());
                }
            }
        }
    }
}
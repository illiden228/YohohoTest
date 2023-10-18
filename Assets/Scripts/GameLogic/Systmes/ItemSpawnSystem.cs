using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic.Systmes
{
    public class ItemSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<ItemSpawnerTag>> _itemSpawnerFilter = default;
        private readonly EcsPoolInject<CooldownTimeComponent> _colldownTimesPool = default;
        private readonly EcsPoolInject<BagComponent> _bagPool = default;
        private readonly EcsPoolInject<ItemTag> _itemPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var itemSpawnerEntity in _itemSpawnerFilter.Value)
            {
                ref var cooldown = ref _colldownTimesPool.Value.Get(itemSpawnerEntity);
                
                if(!cooldown.IsReady)
                    continue;

                ref var bag = ref _bagPool.Value.Get(itemSpawnerEntity);
                
                if(bag.maxCount == bag.items.Count)
                    continue;
                
                bag.items.Push(CreateItem());

                cooldown.IsReady = false;
                cooldown.CurrentTime = 0;
            }
        }

        private EcsPackedEntity CreateItem()
        {
            var itemEntity = _world.Value.NewEntity();
            _itemPool.Value.Add(itemEntity);
            return _world.Value.PackEntity(itemEntity);
        }
    }
}
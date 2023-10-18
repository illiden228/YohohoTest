using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameLogic.Systmes
{
    public class SpawnCooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CooldownTimeComponent>> _colldownTimesFilter;
        private readonly EcsPoolInject<CooldownTimeComponent> _colldownTimesPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var timeEntity in _colldownTimesFilter.Value)
            {
                ref var cooldownComponent = ref _colldownTimesPool.Value.Get(timeEntity);
                if(cooldownComponent.IsReady)
                    continue;
                
                cooldownComponent.CurrentTime += Time.deltaTime;
                if (cooldownComponent.CurrentTime >= cooldownComponent.MaxTime)
                {
                    cooldownComponent.IsReady = true;
                }
            }
        }
    }
}
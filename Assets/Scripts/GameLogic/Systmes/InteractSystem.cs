using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GameLogic.Systmes
{
    public class InteractSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InteractComponent>> _interractFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTag>> _playerFilter = default;
        private readonly EcsFilterInject<Inc<InteractEvent>> _interractEventFilter = default;
        private readonly EcsPoolInject<PositionComponent> _positionPool;
        private readonly EcsPoolInject<InteractComponent> _interactPool;
        private readonly EcsPoolInject<InteractEvent> _interactEventPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var interractEntity in _interractFilter.Value)
            {
                foreach (var playerEntity in _playerFilter.Value)
                {
                    ref var interractPosition = ref _positionPool.Value.Get(interractEntity);
                    ref var interact = ref _interactPool.Value.Get(interractEntity);
                    ref var playerPosition = ref _positionPool.Value.Get(playerEntity);

                    float sqrDistance = (interractPosition.position - playerPosition.position).sqrMagnitude;
                    float sqrRadius = interact.InteractRadius * interact.InteractRadius;
                    if (sqrDistance < sqrRadius)
                    {
                        if (!_interactEventPool.Value.Has(interractEntity))
                        {
                            _interactEventPool.Value.Add(interractEntity);
                        }
                    }
                    else
                    {
                        if (_interactEventPool.Value.Has(interractEntity))
                        {
                            _interactEventPool.Value.Del(interractEntity);
                        }
                    }
                }
            }
        }
    }
}
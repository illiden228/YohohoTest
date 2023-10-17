using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameLogic.Systmes
{
    public class PlayerViewSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<Transform> _playerTransform;
        private readonly EcsFilterInject<Inc<PlayerTag, PositionComponent>> _filter = default;
        private readonly EcsPoolInject<PositionComponent> _positionPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _filter.Value)
            {
                _playerTransform.Value.position = _positionPool.Value.Get(playerEntity).Position;
            }
        }
    }
}
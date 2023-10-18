using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameView.Systmes
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
                Vector3 direction = _positionPool.Value.Get(playerEntity).position - _playerTransform.Value.position;
                _playerTransform.Value.position = _positionPool.Value.Get(playerEntity).position;
                if(direction != Vector3.zero)
                    _playerTransform.Value.forward = direction;
            }
        }
    }
}
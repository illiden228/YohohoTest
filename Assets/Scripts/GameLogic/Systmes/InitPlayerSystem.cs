using Core;
using GameLogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameLogic.Systmes
{
    public class InitPlayerSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<PlayerTag> _playerPool = default;
        private readonly EcsPoolInject<PositionComponent> _positionPool = default;
        private readonly EcsCustomInject<Transform> _startPosition;
        
        public void Init(IEcsSystems systems)
        {
            var playerEntity = _world.Value.NewEntity();
            _playerPool.Value.Add(playerEntity);
            _positionPool.Value.Add(playerEntity).Position = _startPosition.Value.position;
        }
    }
}
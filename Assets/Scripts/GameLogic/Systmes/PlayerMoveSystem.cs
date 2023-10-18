using DefaultNamespace;
using GameLogic.Components;
using Joystick_Pack.Scripts.Joysticks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameLogic.Systmes
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTag, PositionComponent>> _filter = default;
        private readonly EcsPoolInject<PositionComponent> _positionPool = default;
        private readonly EcsCustomInject<IJoystickInput> _joystickInput = default;
        private readonly EcsCustomInject<GameSettings> _settings = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _filter.Value)
            {
                Vector3 direction = new Vector3(_joystickInput.Value.Direction.x, 0, _joystickInput.Value.Direction.y);
                _positionPool.Value.Get(playerEntity).position += direction * _settings.Value.MoveSpeed * Time.deltaTime;
            }
        }
    }
}
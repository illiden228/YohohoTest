using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _itemSpawnCooldown;
        [SerializeField] private int _playerStackMaxCount;
        [SerializeField] private int _itemSpawnerStackMaxCount;
        [SerializeField] private int _itemRecieverStackMaxCount;
        [SerializeField] private float _interactRadius;
        
        public float MoveSpeed => _moveSpeed;
        public float ItemSpawnCooldown => _itemSpawnCooldown;

        public int PlayerStackMaxCount => _playerStackMaxCount;

        public int ItemSpawnerStackMaxCount => _itemSpawnerStackMaxCount;
        public int ItemRecieverStackMaxCount => _itemRecieverStackMaxCount;

        public float InteractRadius => _interactRadius;
    }
}
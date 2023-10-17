using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;

        public float MoveSpeed => _moveSpeed;
    }
}
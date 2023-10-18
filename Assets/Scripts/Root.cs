using DefaultNamespace;
using GameLogic;
using GameView.View;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _itemSpawner;
    [SerializeField] private Transform _itemReciever;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameSettings _settings;
    [SerializeField] private ItemSpawnerView _spawnerView;
    [SerializeField] private ItemRecieverView _recieverView;
    [SerializeField] private PlayerItemView _playerView;
    private EcsStartup _startup;
    
    private void Awake()
    {
        GameStartData startData = new GameStartData
        {
            StartPosition = _player.position,
            StartItemSpawnerPosition = _itemSpawner.position,
            StartItemRecieverPosition = _itemReciever.position
        };
        
        _startup = new EcsStartup(new EcsStartup.Ctx
        {
            playerTransform = _player,
            joystickInput = _joystick,
            settings = _settings,
            gameStartData = startData,
            spawnerView = _spawnerView,
            recieverView = _recieverView,
            playerView = _playerView
        });
    }

    private void Update()
    {
        _startup.Run();
    }

    private void OnDestroy()
    {
        _startup.Destroy();
    }
}

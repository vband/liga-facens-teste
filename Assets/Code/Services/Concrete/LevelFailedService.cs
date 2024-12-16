using System;
using Code.Actors.Abstraction;
using Code.Actors.Concrete;
using Code.Behaviours.Abstraction;
using Code.Services.Abstraction;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Code.Services.Concrete
{
    public class LevelFailedService : ILevelFailedService
    {
        public event Action<int> OnLevelFailed;

        private readonly ILevelScenesService _levelScenesService;

        private KillableActor _playerKillableActor;

        public LevelFailedService(ILevelScenesService levelScenesService)
        {
            _levelScenesService = levelScenesService;
            
            BindPlayerKillableActor();
            SceneManager.sceneLoaded += (_, _) => BindPlayerKillableActor();
        }

        private void BindPlayerKillableActor()
        {
            _playerKillableActor = Object.FindObjectOfType<PlayerActor>();

            if (_playerKillableActor == null)
                return;

            if (!_playerKillableActor.TryGetBehaviour<IKillableBehaviour>(out var killableBehaviour))
                return;
            
            killableBehaviour.OnDeathNotification += InvokeLevelFailed;
        }

        public void InvokeLevelFailed()
            => OnLevelFailed?.Invoke(_levelScenesService.CurrentLevelIndex);
    }
}
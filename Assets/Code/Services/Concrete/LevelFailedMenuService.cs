using System;
using Code.Actors.Abstraction;
using Code.Actors.Concrete;
using Code.Services.Abstraction;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Code.Services.Concrete
{
    public class LevelFailedMenuService : ILevelFailedMenuService
    {
        public event Action OnLevelFailedMenuInvoked;

        private KillableActor _playerKillableActor;

        public LevelFailedMenuService()
        {
            BindPlayerKillableActor();
            SceneManager.sceneLoaded += (_, _) => BindPlayerKillableActor();
        }

        private void BindPlayerKillableActor()
        {
            _playerKillableActor = Object.FindObjectOfType<PlayerActor>();

            if (_playerKillableActor == null)
                return;
            
            _playerKillableActor.KillableBehaviour.OnDied += InvokeLevelFailedMenu;
        }

        public void InvokeLevelFailedMenu()
            => OnLevelFailedMenuInvoked?.Invoke();
    }
}
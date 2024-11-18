using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menus.LevelFailed
{
    public class LevelFailedMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Button _restartLevelButton;

        private ILevelFailedService _levelFailedService;
        private ILevelScenesService _levelScenesService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFailedService = ServiceLocator.Get<ILevelFailedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            
            _levelFailedService.OnLevelFailedInvoked += OpenMenu;
            
            _restartLevelButton.onClick.AddListener(RestartLevel);
        }

        private void OpenMenu()
            => _canvas.SetActive(true);

        private void RestartLevel()
            => _levelScenesService.RestartCurrentLevel();

        private void OnDestroy()
        {
            _levelFailedService.OnLevelFailedInvoked -= OpenMenu;
        }
    }
}
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
        [SerializeField] private Button _startMenuButton;

        private ILevelFailedService _levelFailedService;
        private ILevelScenesService _levelScenesService;
        private IStartMenuSceneService _startMenuSceneService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFailedService = ServiceLocator.Get<ILevelFailedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _startMenuSceneService = ServiceLocator.Get<IStartMenuSceneService>();
            
            _levelFailedService.OnLevelFailed += OpenMenu;
            
            _restartLevelButton.onClick.AddListener(RestartLevel);
            _startMenuButton.onClick.AddListener(GoToStartMenu);
        }

        private void OpenMenu()
            => _canvas.SetActive(true);

        private void RestartLevel()
            => _levelScenesService.RestartCurrentLevel();

        private void GoToStartMenu()
            => _startMenuSceneService.LoadStartMenu();

        private void OnDestroy()
        {
            _levelFailedService.OnLevelFailed -= OpenMenu;
        }
    }
}
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menus.LevelFinished
{
    public class LevelFinishedMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartLevelButton;

        private ILevelFinishedMenuService _levelFinishedMenuService;
        private ILevelScenesService _levelScenesService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFinishedMenuService = ServiceLocator.Get<ILevelFinishedMenuService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            
            _levelFinishedMenuService.OnLevelFinishedMenuInvoked += InvokeMenu;
            
            _nextLevelButton.onClick.AddListener(GoToNextLevel);
            _restartLevelButton.onClick.AddListener(RestartLevel);

            _nextLevelButton.interactable = !_levelScenesService.IsLastLevel;
        }

        private void InvokeMenu()
            => _canvas.SetActive(true);

        private void GoToNextLevel()
            => _levelScenesService.LoadNextLevel();

        private void RestartLevel()
            => _levelScenesService.RestartCurrentLevel();

        private void OnDestroy()
        {
            _levelFinishedMenuService.OnLevelFinishedMenuInvoked -= InvokeMenu;
        }
    }
}
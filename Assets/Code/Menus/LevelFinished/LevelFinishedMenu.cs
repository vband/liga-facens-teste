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
        private ILevelService _levelService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFinishedMenuService = ServiceLocator.Get<ILevelFinishedMenuService>();
            _levelService = ServiceLocator.Get<ILevelService>();
            
            _levelFinishedMenuService.OnLevelFinishedMenuInvoked += InvokeMenu;
            
            _nextLevelButton.onClick.AddListener(GoToNextLevel);
            _restartLevelButton.onClick.AddListener(RestartLevel);

            _nextLevelButton.interactable = !_levelService.IsLastLevel;
        }

        private void InvokeMenu()
            => _canvas.SetActive(true);

        private void GoToNextLevel()
            => _levelService.LoadNextLevel();

        private void RestartLevel()
            => _levelService.RestartCurrentLevel();

        private void OnDestroy()
        {
            _levelFinishedMenuService.OnLevelFinishedMenuInvoked -= InvokeMenu;
        }
    }
}
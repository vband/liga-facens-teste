using Code.Menus.LevelFinished.Views.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menus.LevelFinished
{
    public class LevelFinishedMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private PostLevelView _postLevelView;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _startMenuButton;

        private ILevelFinishedService _levelFinishedService;
        private ILevelScenesService _levelScenesService;
        private ILevelModelsService _levelModelsService;
        private IStartMenuSceneService _startMenuSceneService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFinishedService = ServiceLocator.Get<ILevelFinishedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _levelModelsService = ServiceLocator.Get<ILevelModelsService>();
            _startMenuSceneService = ServiceLocator.Get<IStartMenuSceneService>();
            
            _levelFinishedService.OnLevelFinished += OpenMenu;
            
            _nextLevelButton.onClick.AddListener(GoToNextLevel);
            _restartLevelButton.onClick.AddListener(RestartLevel);
            _startMenuButton.onClick.AddListener(GoToStartMenu);

            _nextLevelButton.interactable = !_levelScenesService.IsLastLevel;
        }

        private void OpenMenu()
        {
            _canvas.SetActive(true);
            UpdatePostLevelView();
        }

        private void UpdatePostLevelView()
        {
            var currentLevelIndex = _levelScenesService.CurrentLevelIndex;
            var currentLevelModel = _levelModelsService.GetLevelModelFromCache(currentLevelIndex);
            _postLevelView.UpdateWithModel(currentLevelModel);
        }

        private void GoToNextLevel()
            => _levelScenesService.LoadNextLevel();

        private void RestartLevel()
            => _levelScenesService.RestartCurrentLevel();

        private void GoToStartMenu()
            => _startMenuSceneService.LoadStartMenu();

        private void OnDestroy()
        {
            _levelFinishedService.OnLevelFinished -= OpenMenu;
        }
    }
}
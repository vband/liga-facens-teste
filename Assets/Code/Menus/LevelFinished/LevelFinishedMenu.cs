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

        private ILevelFinishedService _levelFinishedService;
        private ILevelScenesService _levelScenesService;
        private ILevelModelsService _levelModelsService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFinishedService = ServiceLocator.Get<ILevelFinishedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _levelModelsService = ServiceLocator.Get<ILevelModelsService>();
            
            _levelFinishedService.OnLevelFinishedInvoked += OpenMenu;
            
            _nextLevelButton.onClick.AddListener(GoToNextLevel);
            _restartLevelButton.onClick.AddListener(RestartLevel);

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

        private void OnDestroy()
        {
            _levelFinishedService.OnLevelFinishedInvoked -= OpenMenu;
        }
    }
}
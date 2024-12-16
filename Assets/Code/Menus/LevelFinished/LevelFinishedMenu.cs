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
        private IAdsService _adsService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFinishedService = ServiceLocator.Get<ILevelFinishedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _levelModelsService = ServiceLocator.Get<ILevelModelsService>();
            _startMenuSceneService = ServiceLocator.Get<IStartMenuSceneService>();
            _adsService = ServiceLocator.Get<IAdsService>();
            
            _levelFinishedService.OnLevelFinished += OpenMenu;
            
            _nextLevelButton.onClick.AddListener(GoToNextLevelAfterAd);
            _restartLevelButton.onClick.AddListener(RestartLevelAfterAd);
            _startMenuButton.onClick.AddListener(GoToStartMenuAfterAd);

            _nextLevelButton.interactable = !_levelScenesService.IsLastLevel;
        }

        private void OpenMenu(int _)
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

        private void GoToNextLevelAfterAd()
            => _adsService.DoAfterInterstitialAd(() => _levelScenesService.LoadNextLevel());

        private void RestartLevelAfterAd()
            => _adsService.DoAfterInterstitialAd(() => _levelScenesService.RestartCurrentLevel());

        private void GoToStartMenuAfterAd()
            => _adsService.DoAfterInterstitialAd(() => _startMenuSceneService.LoadStartMenu());

        private void OnDestroy()
        {
            _levelFinishedService.OnLevelFinished -= OpenMenu;
        }
    }
}
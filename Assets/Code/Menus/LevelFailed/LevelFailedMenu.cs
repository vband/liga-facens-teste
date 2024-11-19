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
        private IAdsService _adsService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFailedService = ServiceLocator.Get<ILevelFailedService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _startMenuSceneService = ServiceLocator.Get<IStartMenuSceneService>();
            _adsService = ServiceLocator.Get<IAdsService>();
            
            _levelFailedService.OnLevelFailed += OpenMenu;
            
            _restartLevelButton.onClick.AddListener(RestartLevelAfterAd);
            _startMenuButton.onClick.AddListener(GoToStartMenuAfterAd);
        }

        private void OpenMenu()
            => _canvas.SetActive(true);

        private void RestartLevelAfterAd()
            => _adsService.DoAfterInterstitialAd(() => _levelScenesService.RestartCurrentLevel());

        private void GoToStartMenuAfterAd()
            => _adsService.DoAfterInterstitialAd(() => _startMenuSceneService.LoadStartMenu());

        private void OnDestroy()
        {
            _levelFailedService.OnLevelFailed -= OpenMenu;
        }
    }
}
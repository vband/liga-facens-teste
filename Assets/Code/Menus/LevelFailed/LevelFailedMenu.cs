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

        private ILevelFailedMenuService _levelFailedMenuService;
        private ILevelScenesService _levelScenesService;

        private void Start()
        {
            _canvas.SetActive(false);
            
            _levelFailedMenuService = ServiceLocator.Get<ILevelFailedMenuService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            
            _levelFailedMenuService.OnLevelFailedMenuInvoked += InvokeMenu;
            
            _restartLevelButton.onClick.AddListener(RestartLevel);
        }

        private void InvokeMenu()
            => _canvas.SetActive(true);

        private void RestartLevel()
            => _levelScenesService.RestartCurrentLevel();

        private void OnDestroy()
        {
            _levelFailedMenuService.OnLevelFailedMenuInvoked -= InvokeMenu;
        }
    }
}
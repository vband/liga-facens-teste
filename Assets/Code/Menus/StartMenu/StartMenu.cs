using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menus.StartMenu
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private ILevelSelectionMenuService _levelSelectionMenuService;
        
        private void Start()
        {
            _levelSelectionMenuService = ServiceLocator.Get<ILevelSelectionMenuService>();
            
            _playButton.onClick.AddListener(InvokeLevelSelectionMenu);
        }

        private void InvokeLevelSelectionMenu()
            => _levelSelectionMenuService.InvokeLevelSelectionMenu();
    }
}

using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menus.StartMenu
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        
        private ILevelService _levelService;
        
        private void Start()
        {
            _levelService = ServiceLocator.Get<ILevelService>();
            
            _playButton.onClick.AddListener(StartFirstLevel);
        }

        private void StartFirstLevel()
            => _levelService.LoadFirstLevel();
    }
}

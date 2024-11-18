using System.Collections.Generic;
using Code.Menus.LevelSelection.Presenters.Concrete;
using Code.Menus.LevelSelection.Views.Concrete;
using Code.Models.Abstraction;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menus.LevelSelection
{
    public class LevelSelectionMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private List<LevelView> _serializedLevelViews = new();
        [SerializeField] private Button _goBackButton;

        private ILevelSelectionMenuService _levelSelectionMenuService;
        private ILevelModelsService _levelModelsService;
        private ILevelScenesService _levelScenesService;

        private List<ILevelModel> _levelModels;

        private async void Start()
        {
            _levelSelectionMenuService = ServiceLocator.Get<ILevelSelectionMenuService>();
            _levelModelsService = ServiceLocator.Get<ILevelModelsService>();
            _levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            
            _levelModels = await _levelModelsService.LoadLevelModelsAsync();

            _levelSelectionMenuService.OnLevelSelectionMenuInvoked += OpenMenu;
            _goBackButton.onClick.AddListener(CloseMenu);

            DeleteExcessLevelViewsIfNeeded();
            CreateNewLevelViewsIfNeeded();

            BindPresenters();
            CloseMenu();
        }

        private void OpenMenu()
            => _canvas.gameObject.SetActive(true);

        private void CloseMenu()
            => _canvas.gameObject.SetActive(false);

        private void CreateNewLevelViewsIfNeeded()
        {
            if (_serializedLevelViews.Count >= _levelModels.Count)
                return;

            for (var i = _serializedLevelViews.Count; i <= _levelModels.Count - 1; i++)
            {
                var newView = Instantiate(_serializedLevelViews[^1],
                    _serializedLevelViews[^1].gameObject.transform.parent);
                _serializedLevelViews.Add(newView);
            }
        }

        private void DeleteExcessLevelViewsIfNeeded()
        {
            if (_serializedLevelViews.Count <= _levelModels.Count)
                return;
            
            for (var i = _serializedLevelViews.Count - 1; i >= _levelModels.Count - 1; i--)
                Destroy(_serializedLevelViews[i].gameObject);
        }

        private void BindPresenters()
        {
            for (var i = 0; i < _levelModels.Count; i++)
            {
                var presenter = new LevelPresenter(_levelModels[i], _serializedLevelViews[i], _levelScenesService);
                _serializedLevelViews[i].BindPresenter(presenter);
            }
        }

        private void OnDestroy()
        {
            _levelSelectionMenuService.OnLevelSelectionMenuInvoked -= OpenMenu;
        }
    }
}

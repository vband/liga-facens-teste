using Code.Menus.LevelSelection.Presenters.Abstraction;
using Code.Menus.LevelSelection.Views.Abstraction;
using Code.Models.Abstraction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menus.LevelSelection.Views.Concrete
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Button _button;
        
        private ILevelPresenter _presenter;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        public void BindPresenter(ILevelPresenter presenter)
        {
            _presenter = presenter;
        }

        public void UpdateWithModel(ILevelModel model)
        {
            _label.text = model.Label;
            _button.interactable = model.Unlocked;
        }

        private void OnButtonClick()
            => _presenter.SelectLevel();

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
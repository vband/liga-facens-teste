using Code.Menus.LevelFinished.Views.Abstraction;
using Code.Models.Abstraction;
using TMPro;
using UnityEngine;

namespace Code.Menus.LevelFinished.Views.Concrete
{
    public class PostLevelView : MonoBehaviour, IPostLevelView
    {
        [SerializeField] private TextMeshProUGUI _bestCompletionTime;
        [SerializeField] private string _timeValuePostfix = " seconds";

        public void UpdateWithModel(ILevelModel model)
        {
            _bestCompletionTime.text = model.BestCompletionTime.ToString("0.00") + _timeValuePostfix;
        }
    }
}
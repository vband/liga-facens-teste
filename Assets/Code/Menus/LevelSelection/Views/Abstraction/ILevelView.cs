using Code.Menus.LevelSelection.Presenters.Abstraction;
using Code.Models.Abstraction;

namespace Code.Menus.LevelSelection.Views.Abstraction
{
    public interface ILevelView
    {
        void BindPresenter(ILevelPresenter presenter);
        void UpdateWithModel(ILevelModel model);
    }
}
using Code.Models.Abstraction;

namespace Code.Menus.LevelFinished.Views.Abstraction
{
    public interface IPostLevelView
    {
        void UpdateWithModel(ILevelModel model);
    }
}
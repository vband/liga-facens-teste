using Code.Menus.LevelSelection.Presenters.Abstraction;
using Code.Menus.LevelSelection.Views.Abstraction;
using Code.Models.Abstraction;
using Code.Services.Abstraction;

namespace Code.Menus.LevelSelection.Presenters.Concrete
{
    public class LevelPresenter : ILevelPresenter
    {
        private readonly ILevelModel _model;
        private readonly ILevelScenesService _levelScenesService;

        public LevelPresenter(ILevelModel model, ILevelView view, ILevelScenesService levelScenesService)
        {
            _model = model;
            _levelScenesService = levelScenesService;
            view.UpdateWithModel(_model);
        }

        public void SelectLevel()
            => _levelScenesService.LoadLevel(_model.LevelIndex);
    }
}
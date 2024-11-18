using Code.Behaviours.Abstraction;
using Code.Services.Abstraction;

namespace Code.Behaviours.Concrete
{
    public class FinishLevelBehaviour : IFinishLevelBehaviour
    {
        private readonly ILevelFinishedMenuService _levelFinishedMenuService;
        private readonly ILevelScenesService _levelScenesService;
        
        public FinishLevelBehaviour(ILevelFinishedMenuService levelFinishedMenuService,
            ILevelScenesService levelScenesService)
        {
            _levelFinishedMenuService = levelFinishedMenuService;
            _levelScenesService = levelScenesService;
        }
        
        public void FinishCurrentLevel()
        {
            _levelScenesService.UnlockNextLevel();
            _levelFinishedMenuService.InvokeLevelFinishedMenu();
        }
    }
}
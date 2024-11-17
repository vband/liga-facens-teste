using Code.Behaviours.Abstraction;
using Code.Services.Abstraction;

namespace Code.Behaviours.Concrete
{
    public class FinishLevelBehaviour : IFinishLevelBehaviour
    {
        private readonly ILevelFinishedMenuService _levelFinishedMenuService;
        
        public FinishLevelBehaviour(ILevelFinishedMenuService levelFinishedMenuService)
        {
            _levelFinishedMenuService = levelFinishedMenuService;
        }
        
        public void FinishCurrentLevel()
        {
            _levelFinishedMenuService.InvokeLevelFinishedMenu();
        }
    }
}
using Code.Behaviours.Abstraction;
using Code.Services.Abstraction;

namespace Code.Behaviours.Concrete
{
    public class FinishLevelBehaviour : IFinishLevelBehaviour
    {
        private readonly ILevelFinishedService _levelFinishedService;
        private readonly ILevelScenesService _levelScenesService;
        private readonly ILevelModelsService _levelModelsService;
        
        public FinishLevelBehaviour(ILevelFinishedService levelFinishedService,
            ILevelScenesService levelScenesService, ILevelModelsService levelModelsService)
        {
            _levelFinishedService = levelFinishedService;
            _levelScenesService = levelScenesService;
            _levelModelsService = levelModelsService;
        }
        
        public void FinishCurrentLevel()
        {
            var currentLevelIndex = _levelScenesService.CurrentLevelIndex;
            _levelModelsService.UnlockLevel(currentLevelIndex + 1);
            _levelFinishedService.InvokeLevelFinished();
        }
    }
}
using System;
using Code.Services.Abstraction;

namespace Code.Services.Concrete
{
    public class LevelFinishedService : ILevelFinishedService
    {
        public event Action<int> OnLevelFinished;

        private readonly ILevelScenesService _levelScenesService;

        public LevelFinishedService(ILevelScenesService levelScenesService)
        {
            _levelScenesService = levelScenesService;
        }

        public void InvokeLevelFinished()
            => OnLevelFinished?.Invoke(_levelScenesService.CurrentLevelIndex);
    }
}
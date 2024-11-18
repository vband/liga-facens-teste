using System;
using Code.Services.Abstraction;

namespace Code.Services.Concrete
{
    public class LevelFinishedService : ILevelFinishedService
    {
        public event Action OnLevelFinished;

        public void InvokeLevelFinished()
            => OnLevelFinished?.Invoke();
    }
}
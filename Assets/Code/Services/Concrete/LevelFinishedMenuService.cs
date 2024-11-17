using System;
using Code.Services.Abstraction;

namespace Code.Services.Concrete
{
    public class LevelFinishedMenuService : ILevelFinishedMenuService
    {
        public event Action OnLevelFinishedMenuInvoked;

        public void InvokeLevelFinishedMenu()
            => OnLevelFinishedMenuInvoked?.Invoke();
    }
}
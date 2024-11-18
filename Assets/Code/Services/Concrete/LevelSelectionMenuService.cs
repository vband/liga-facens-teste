using System;
using Code.Services.Abstraction;

namespace Code.Services.Concrete
{
    public class LevelSelectionMenuService : ILevelSelectionMenuService
    {
        public event Action OnLevelSelectionMenuInvoked;

        public void InvokeLevelSelectionMenu()
            => OnLevelSelectionMenuInvoked?.Invoke();
    }
}
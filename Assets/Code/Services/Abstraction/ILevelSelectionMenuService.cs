using System;

namespace Code.Services.Abstraction
{
    public interface ILevelSelectionMenuService : IService
    {
        event Action OnLevelSelectionMenuInvoked;
        void InvokeLevelSelectionMenu();
    }
}
using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFinishedMenuService : IService
    {
        event Action OnLevelFinishedMenuInvoked;
        void InvokeLevelFinishedMenu();
    }
}
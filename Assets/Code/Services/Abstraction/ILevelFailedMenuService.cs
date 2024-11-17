using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFailedMenuService : IService
    {
        event Action OnLevelFailedMenuInvoked;
        void InvokeLevelFailedMenu();
    }
}
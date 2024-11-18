using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFailedService : IService
    {
        event Action OnLevelFailedInvoked;
        void InvokeLevelFailed();
    }
}
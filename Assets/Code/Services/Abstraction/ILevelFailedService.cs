using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFailedService : IService
    {
        event Action OnLevelFailed;
        void InvokeLevelFailed();
    }
}
using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFailedService : IService
    {
        event Action<int> OnLevelFailed;
        void InvokeLevelFailed();
    }
}
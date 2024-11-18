using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFinishedService : IService
    {
        event Action OnLevelFinishedInvoked;
        void InvokeLevelFinished();
    }
}
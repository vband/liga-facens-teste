using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFinishedService : IService
    {
        event Action OnLevelFinished;
        void InvokeLevelFinished();
    }
}
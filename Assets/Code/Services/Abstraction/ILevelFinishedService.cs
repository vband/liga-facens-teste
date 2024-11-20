using System;

namespace Code.Services.Abstraction
{
    public interface ILevelFinishedService : IService
    {
        event Action<int> OnLevelFinished;
        void InvokeLevelFinished();
    }
}
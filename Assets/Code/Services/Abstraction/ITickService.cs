using System;

namespace Code.Services.Abstraction
{
    public interface ITickService : IService
    {
        event Action OnTick;
    }
}
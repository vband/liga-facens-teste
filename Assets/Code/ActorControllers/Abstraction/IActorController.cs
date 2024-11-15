using System;

namespace Code.ActorControllers.Abstraction
{
    public interface IActorController : IDisposable
    {
        void SetEnabled(bool enabled);
    }
}

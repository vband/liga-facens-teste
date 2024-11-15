using System;

namespace Code.ActorControllers.Abstraction
{
    public interface IRunnerActorController : IActorController
    {
        event Action<float> OnRunAction;
    }
}
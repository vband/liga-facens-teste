using System;

namespace Code.ActorControllers.Abstraction
{
    public interface IJumperActorController : IActorController
    {
        event Action<bool> OnJumpAction;
    }
}
using System;

namespace Code.ActorControllers.Abstraction
{
    public interface IActorController
    {
        event Action<float> OnMoveAction;
        event Action<bool> OnJumpAction;
        void SetEnabled(bool enabled);
    }
}

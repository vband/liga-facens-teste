using System;

namespace Code.Input.Base
{
    public interface IPlayerInputController
    {
        event Action<float> OnMoveAction;
        event Action<bool> OnJumpAction;
        void SetEnabled(bool enabled);
    }
}

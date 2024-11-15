using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using UnityEngine.InputSystem;

namespace Code.ActorControllers.Concrete
{
    public class InputActorController : IActorController
    {
        private readonly PlayerInputActions _playerInputActions;
        private readonly IRunnerJumperActor _actor;

        public InputActorController(IRunnerJumperActor actor)
        {
            _actor = actor;
            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Player.Move.performed += OnMovePerformed;
            _playerInputActions.Player.Move.canceled += OnMoveCanceled;
            _playerInputActions.Player.Jump.performed += OnJumpPerformed;
            _playerInputActions.Player.Jump.canceled += OnJumpCanceled;
        }

        public void SetEnabled(bool enabled)
        {
            if (enabled)
                _playerInputActions.Enable();
            else
                _playerInputActions.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
            => _actor.UpdateMovement(context.ReadValue<float>());

        private void OnMoveCanceled(InputAction.CallbackContext context)
            => _actor.UpdateMovement(0);

        private void OnJumpPerformed(InputAction.CallbackContext context)
            => _actor.UpdateJump(true);

        private void OnJumpCanceled(InputAction.CallbackContext context)
            => _actor.UpdateJump(false);

        public void Dispose()
            => _playerInputActions?.Dispose();
    }
}

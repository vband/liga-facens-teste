using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Services.Abstraction;
using UnityEngine.InputSystem;

namespace Code.ActorControllers.Concrete
{
    public class InputActorController : IActorController
    {
        private readonly PlayerInputActions _playerInputActions;
        private readonly IRunnerJumperActor _actor;
        private readonly ITickService _tickService;

        private float _movementAxis;
        private bool _jumping;

        public InputActorController(IRunnerJumperActor actor, ITickService tickService)
        {
            _actor = actor;
            _tickService = tickService;
            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Player.Move.performed += OnMovePerformed;
            _playerInputActions.Player.Move.canceled += OnMoveCanceled;
            _playerInputActions.Player.Jump.performed += OnJumpPerformed;
            _playerInputActions.Player.Jump.canceled += OnJumpCanceled;
            _tickService.OnTick += OnTick;
        }

        public void SetEnabled(bool enabled)
        {
            if (enabled)
                _playerInputActions.Enable();
            else
            {
                _actor.UpdateMovement(0);
                _actor.UpdateJump(false);
                _playerInputActions.Disable();
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
            => _movementAxis = context.ReadValue<float>();

        private void OnMoveCanceled(InputAction.CallbackContext context)
            => _movementAxis = 0;

        private void OnJumpPerformed(InputAction.CallbackContext context)
            => _jumping = true;

        private void OnJumpCanceled(InputAction.CallbackContext context)
            => _jumping = false;

        public void Dispose()
        {
            _playerInputActions.Player.Move.performed += OnMovePerformed;
            _playerInputActions.Player.Move.canceled += OnMoveCanceled;
            _playerInputActions.Player.Jump.performed += OnJumpPerformed;
            _playerInputActions.Player.Jump.canceled += OnJumpCanceled;
            _tickService.OnTick -= OnTick;
            
            _playerInputActions?.Dispose();
        }

        private void OnTick()
        {
            _actor.UpdateMovement(_movementAxis);
            _actor.UpdateJump(_jumping);
        }
    }
}

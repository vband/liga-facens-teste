using System;
using Code.Input.Base;
using UnityEngine.InputSystem;

namespace Code.Input.Implementations
{
    public class InputSystemController : IPlayerInputController
    {
        public event Action<float> OnMoveAction;
        public event Action<bool> OnJumpAction;

        private readonly PlayerInputActions _playerInputActions;

        public InputSystemController()
        {
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
            => OnMoveAction?.Invoke(context.ReadValue<float>());

        private void OnMoveCanceled(InputAction.CallbackContext context)
            => OnMoveAction?.Invoke(0);

        private void OnJumpPerformed(InputAction.CallbackContext context)
            => OnJumpAction?.Invoke(true);

        private void OnJumpCanceled(InputAction.CallbackContext context)
            => OnJumpAction?.Invoke(false);
    }
}

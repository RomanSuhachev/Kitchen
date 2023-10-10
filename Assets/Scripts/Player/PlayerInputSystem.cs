using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputSystem : MonoBehaviour
    {

        public event EventHandler OnInterAction;
        public event EventHandler OnInteractAlternateAction;
        private PlayerInputActions _playerInputActions;
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Interact.performed += Interact_performed;
            _playerInputActions.Player.InteractionAlternate.performed += InteractionAlternate_performed;
        }

        private void InteractionAlternate_performed(InputAction.CallbackContext obj) =>
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

        private void Interact_performed(InputAction.CallbackContext obj) => OnInterAction?.Invoke(this, EventArgs.Empty);

        public Vector2 GetMovementNormalized()
        {
            
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }
    }
}

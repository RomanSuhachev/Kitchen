using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class GameInput : MonoBehaviour
    {

        public event EventHandler OnInterAction;
        private PlayerInputActions _playerInputActions;
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Interact.performed += Interact_performed;
        }

        private void Interact_performed(InputAction.CallbackContext obj) => OnInterAction?.Invoke(this, EventArgs.Empty);

        public Vector2 GetMovementNormalized()
        {
            
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }
    }
}

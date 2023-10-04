using System;
using UnityEngine;

namespace Player
{
    public class PlayerInputSystem : MonoBehaviour
    {

        private PlayerInputActions _playerInputActions;
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        }

        public Vector2 GetMovementNormalized()
        {
            
            Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }
    }
}

using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour
    {
    
        [SerializeField] private float speed = 20f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private GameInput gameInput;
        public bool IsWalking { get; private set; }

        private void Update()
        {
            Vector2 inputVector = gameInput.GetMovementNormalized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            transform.position += moveDir * (speed * Time.deltaTime);

            IsWalking = moveDir != Vector3.zero;
        
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }
    }
}

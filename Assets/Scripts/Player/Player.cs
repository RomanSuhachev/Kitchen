using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour
    {
    
        [SerializeField] private float speed = 20f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private GameInput gameInput;
        [SerializeField] private CharacterController charContr;
        public bool IsWalking { get; private set; }

        private void Awake()
        {
            charContr = GetComponent<CharacterController>();
        }

        private void Update()
        {
            float moveDistance = speed * Time.deltaTime;
            
            Vector2 inputVector = gameInput.GetMovementNormalized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * charContr.height, charContr.radius,
                moveDir, moveDistance);
            if (!canMove)
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * charContr.height, charContr.radius,
                    moveDirX, moveDistance);
            
                if (canMove)
                {
                    moveDir = moveDirX;
                }else
                {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * charContr.height, charContr.radius,
                        moveDirZ, moveDistance);
                    if (canMove)
                    {
                        moveDir = moveDirZ;
                    }
                }
            
                
            }
            
            if (canMove)
            {
                transform.position += moveDir * moveDistance;
            }

            IsWalking = moveDir != Vector3.zero;
        
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }
    }
}

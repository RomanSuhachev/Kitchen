using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IKitchenObjectParent
    {
        public static Player Instance { get; private set; }

        public event EventHandler OnPickedSomething;
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

        public class OnSelectedCounterChangedEventArgs : EventArgs
        {
            public BaseCounter selectedCounter;
        }
    
        [SerializeField] private float speed = 20f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private PlayerInputSystem playerInput;
        [SerializeField] private CharacterController charContr;
        [SerializeField] private LayerMask counterLayerMask;
        [SerializeField] private Transform kitchenObjectHoldPoint;
        public bool IsWalking { get; private set; }
        private BaseCounter selectedCounter;
        private Vector3 lastInteractDir;
        private KitchenObject kitchenObject;


        private void Start()
        {
            playerInput.OnInterAction += GameInput_OnInterAction;
            playerInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        }

        private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsGamePlaying()) return;
           selectedCounter?.InteractAlternate(this);
        }

        private void GameInput_OnInterAction(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsGamePlaying()) return;

            if (selectedCounter != null)
            {
                selectedCounter.Interact(this);
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void HandleInteractions()
        {
            Vector2 inputVector = playerInput.GetMovementNormalized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            float interactDistance = 4f;
            RaycastHit raycastHit;

            if (moveDir != Vector3.zero)
            {
                lastInteractDir = moveDir;
            }

            if (Physics.Raycast(transform.position, lastInteractDir,out raycastHit, interactDistance, counterLayerMask))
            {
                
                if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (baseCounter != selectedCounter)
                    {
                        SetSelectedCounter(baseCounter);
                        
                        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                        {
                            selectedCounter = selectedCounter
                        });
                    }
                    
                }else
                {
                    SetSelectedCounter(null);
                }
            }else
            {
                SetSelectedCounter(null);
            }
        }

        private void HandleMovement()
        {
            float moveDistance = speed * Time.deltaTime;
            
            Vector2 inputVector = playerInput.GetMovementNormalized();
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * charContr.height, charContr.radius,
                moveDir, moveDistance);
            if (!canMove)
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = moveDir.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * charContr.height, charContr.radius,
                    moveDirX, moveDistance);
            
                if (canMove)
                {
                    moveDir = moveDirX;
                }else
                {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * charContr.height, charContr.radius,
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
        
        private void SetSelectedCounter(BaseCounter selectedCounter)
        {
            this.selectedCounter = selectedCounter;
        
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                selectedCounter = selectedCounter
            });
        }

        public Transform GetKitchenObjectFollowTransform() => kitchenObjectHoldPoint;

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this.kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                OnPickedSomething?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject GetKitchenObject() => kitchenObject;

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public bool HasKitchenObject() => kitchenObject != null;
    }
}

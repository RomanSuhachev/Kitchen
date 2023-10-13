using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    
    [SerializeField] private Transform cleaCounterTopPoint;

    private KitchenObject _kitchenObject;
    public virtual void Interact(Player.Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    
    public virtual void InteractAlternate(Player.Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }
    
    public Transform GetKitchenObjectFollowTransform() => cleaCounterTopPoint;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() => _kitchenObject;

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject() => _kitchenObject != null;
}

using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

   [SerializeField] private KitchenObjectSO kitchenObjectSo;
   [SerializeField] private Transform cleaCounterTopPoint;

   private KitchenObject _kitchenObject;
   
    public void Interact(Player.Player player)
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab, cleaCounterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            _kitchenObject.SetKitchenObjectParent(player);
        }
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

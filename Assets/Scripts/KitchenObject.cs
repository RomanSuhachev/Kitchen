using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter _clearCounter;

    public KitchenObjectSO GetKitechenObjectSO() => kitchenObjectSO;


    public void SetCleatCounter(ClearCounter clearCounter)
    {
        if(this._clearCounter != null)
        {
            this._clearCounter.clearKitchenObject();
        }
        this._clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject");
        }
        clearCounter.SetKitchenObject(this);
        
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    } 
    public ClearCounter GetClearCounter() => _clearCounter;
   
}
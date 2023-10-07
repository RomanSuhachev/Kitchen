using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

   [SerializeField] private KitchenObjectSO kitchenObjectSo;
   [SerializeField] private Transform cleaCounterTopPoint;
   [SerializeField] private ClearCounter secondClearCounter;
   [SerializeField] private bool testing;

   private KitchenObject kitchenObject;

   private KitchenObject KitchenObject;

   private void Update()
   {
       if (testing && Input.GetKeyDown(KeyCode.T))
       {
           if (kitchenObject != null)
           {
               kitchenObject.SetCleatCounter(secondClearCounter);
           }
       }
   }
    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab, cleaCounterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetCleatCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
     
    }
    public Transform GetKitchenObjectFollowTransform() => cleaCounterTopPoint;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() => kitchenObject;

    public void clearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject() => kitchenObject != null;
}

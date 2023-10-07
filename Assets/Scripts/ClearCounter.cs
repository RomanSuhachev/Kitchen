using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

   [SerializeField] private KitchenObjectSO kitchenObjectSo;
   [SerializeField] private Transform cleaCounterTopPoint;
    
    
    
    public void Interact()
    {
        Debug.Log("Interact");
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab, cleaCounterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitechenObjectSO().objectName);
    }


}

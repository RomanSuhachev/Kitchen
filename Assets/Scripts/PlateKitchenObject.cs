using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
   public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;

   public class OnIngridientAddedEventArgs : EventArgs
   {
      public KitchenObjectSO kitchenObjectSO;
   }
   
   [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

   private List<KitchenObjectSO> _kitchenObjectSOList;

   private void Awake()
   {
      _kitchenObjectSOList = new List<KitchenObjectSO>();
   }

   
   
   public bool TryAddIngridient(KitchenObjectSO kitchenObjectSO)
   {
      if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
      {
         //Not a valid ingridient
         return false;
      }
      if (_kitchenObjectSOList.Contains(kitchenObjectSO))
      {
         return false;
      }
      else
      {
         _kitchenObjectSOList.Add(kitchenObjectSO);
         OnIngridientAdded?.Invoke(this, new OnIngridientAddedEventArgs
         {
            kitchenObjectSO = kitchenObjectSO
         });
         return true;
      }
   }

   public List<KitchenObjectSO> GetKitchenObjectSOList()
   {
      return _kitchenObjectSOList;
   }
}

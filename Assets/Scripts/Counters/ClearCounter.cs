using UnityEngine;

public class ClearCounter : BaseCounter
{

   [SerializeField] private KitchenObjectSO kitchenObjectSo;

   private KitchenObject _kitchenObject;

   public override void Interact(Player.Player player)
   {
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            player.GetKitchenObject().SetKitchenObjectParent(this);
         }
         else
         {
            
         }
      }
      else
      {
         if (player.HasKitchenObject())
         {
            
         }
         else
         { 
            GetKitchenObject().SetKitchenObjectParent(player);
         }
      }
   }
}

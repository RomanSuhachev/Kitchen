using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : BaseCounter
{

    [SerializeField] private CuttingReceipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public override void Interact(Player.Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasReceipeWithInput(player.GetKitchenObject().GetKitechenObjectSO()))
                {
                    //Player carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                }
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

    public override void InteractAlternate(Player.Player player)
    {
        if (HasKitchenObject() && HasReceipeWithInput(GetKitchenObject().GetKitechenObjectSO()))
        {
            cuttingProgress++;
            
            CuttingReceipeSO cuttingReceipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitechenObjectSO());

            if (cuttingProgress >= cuttingReceipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitechenObjectSO());
                // There is a kitchenObject here & it can be cut
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasReceipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingReceipeSO cuttingReceipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingReceipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingReceipeSO cuttingReceipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingReceipeSO != null)
        {
            return cuttingReceipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingReceipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingReceipeSO cuttingReceipeSo in cuttingRecipeSOArray)
        {
            if (cuttingReceipeSo.input == inputKitchenObjectSO)
            {
                return cuttingReceipeSo;
            }
        }
        return null;
    }
}

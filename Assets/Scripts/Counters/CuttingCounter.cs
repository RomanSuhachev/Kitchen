using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    
    public event EventHandler OnCut;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

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
                    
                    CuttingReceipeSO cuttingReceipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitechenObjectSO());


                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {

                        progressNormalized = (float)cuttingProgress / cuttingReceipeSO.cuttingProgressMax
                    });
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a Plate
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitechenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
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
            
            OnCut?.Invoke(this, EventArgs.Empty);
            
            CuttingReceipeSO cuttingReceipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitechenObjectSO());
            
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {

                progressNormalized = (float)cuttingProgress / cuttingReceipeSO.cuttingProgressMax
            });

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

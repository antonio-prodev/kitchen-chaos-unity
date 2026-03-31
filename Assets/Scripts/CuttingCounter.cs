using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                KitchenObject kitchenObjectFromPlayer = player.GetKitchenObject();
                if (HasCuttingRecipe(kitchenObjectFromPlayer.GetKitchenObjectSO()))
                {
                    kitchenObjectFromPlayer.SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
            else
            {
                // player doesn't have kitchen object
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                // player already has a kitchen object
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO()))
        {
            // slice kitchen object
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                GetKitchenObject().DestroySelf();

                KitchenObjectSO output = cuttingRecipeSO.output;
                KitchenObject.SpawnKitchenObject(output, this);
            }

        }
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    {
        return GetCuttingRecipeSOFromInput(input).output;
    }

    private bool HasCuttingRecipe(KitchenObjectSO kitchenObjectSO)
    {
        return GetCuttingRecipeSOFromInput(kitchenObjectSO) != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO inputKitchenObject)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObject)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}

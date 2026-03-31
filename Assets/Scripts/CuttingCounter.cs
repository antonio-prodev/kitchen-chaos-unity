using UnityEngine;

public class CuttingCounter : BaseCounter
{
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

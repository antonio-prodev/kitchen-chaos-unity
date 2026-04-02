using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeListMax = 4;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeListMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                string waitingList = "";
                foreach(RecipeSO recipeSO in waitingRecipeSOList)
                {
                    waitingList += recipeSO.recipeName + "; ";
                }
                Debug.Log(waitingList);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        bool correctRecipe = false;
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (plateKitchenObject.GetKitchenObjectSOList().Count == waitingRecipeSO.kitchenObjectSOList.Count)
            {
                // fist check : same number of ingredients
                correctRecipe = true;

                foreach (KitchenObjectSO kitchenObjectSOFromPlate in plateKitchenObject.GetKitchenObjectSOList())
                {   
                    // second check : all indredients are correct
                    bool correctIngredient = false;
                    foreach(KitchenObjectSO kitchenObjectSOFromRecipe in waitingRecipeSO.kitchenObjectSOList)
                    {
                        if (kitchenObjectSOFromPlate == kitchenObjectSOFromRecipe)
                        {
                            correctIngredient = true;
                            break;
                        }
                    }
                    if (!correctIngredient)
                    {
                        correctRecipe = false;
                        break;
                    }
                }

                if (correctRecipe)
                {
                    Debug.Log("Player has delivered a correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        if (!correctRecipe)
        {
            Debug.Log("Player has delivered an incorrect recipe");
        }
    }
}

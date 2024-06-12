using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    [SerializeField] private RecipeListSO recipeListSO;


    private List<RecipeSO> ordersRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int maxOrderRecipe = 6;


    private void Awake()
    {
        Instance = this;

        ordersRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(ordersRecipeSOList.Count < maxOrderRecipe)
            {
                RecipeSO orderRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(orderRecipeSO.recipeName);
                ordersRecipeSOList.Add(orderRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < ordersRecipeSOList.Count; i++)
        {
            RecipeSO orderRecipeSO = ordersRecipeSOList[i];

            if(orderRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool itemInPlateAndRecipeMatched = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in orderRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientMatched = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            ingredientMatched = true;
                            break;
                        }
                    }
                    if (!ingredientMatched)
                    {
                        itemInPlateAndRecipeMatched = false;
                    }
                }
                if (itemInPlateAndRecipeMatched)
                {
                    Debug.Log("Player delivered correct orders");
                    ordersRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        Debug.Log("Player delivered incorrect orders");
    }
}

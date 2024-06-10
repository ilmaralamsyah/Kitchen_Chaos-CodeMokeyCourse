using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance;

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool recipeAndDeliveredItemMatches = true;
                foreach (KitchenObjectSO recipeKithcenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientMatches = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(recipeKithcenObjectSO == plateKitchenObjectSO)
                        {
                            ingredientMatches = true;
                            break;
                        }
                    }
                    if (!ingredientMatches)
                    {
                        //ingredient matches
                        recipeAndDeliveredItemMatches = false;
                        break;
                    }
                }
                if (recipeAndDeliveredItemMatches)
                {
                    //player delivered correct item
                    Debug.Log("Player delivered correct item");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        Debug.Log("Player delivered wrong item");
    }
}
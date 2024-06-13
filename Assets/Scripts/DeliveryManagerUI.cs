using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private List<Transform> recipeBackgroundUI;
    [SerializeField] private RecipeContainerUI recipeContainer;

    private void Awake()
    {
        foreach (Transform t in recipeBackgroundUI)
        {
            t.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        DeliveryManager.Instance.OnOrderRecipeSpawned += DeliveryManager_OnOrderRecipeSpawned;
        DeliveryManager.Instance.OnOrderRecipeCompleted += DeliveryManager_OnOrderRecipeCompleted;
    }

    private void DeliveryManager_OnOrderRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnOrderRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            bool shouldDestroy = true;
            foreach (Transform t in recipeBackgroundUI)
            {
                if (child == t)
                {
                    shouldDestroy = false;
                    break;
                }
            }

            if (shouldDestroy)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetOrderRecipeSOList())
        {
            int totalIngredient = recipeSO.kitchenObjectSOList.Count;
            foreach(RecipeContainerUIType recipeContainerUIType in recipeContainer.GetRecipeContainerUIList())
            {
                if (recipeContainerUIType.GetRecipeContainerUIType() == totalIngredient)
                {
                    Transform recipeContainerTransform = Instantiate(recipeContainerUIType.transform, container);
                    recipeContainerTransform.gameObject.SetActive(true);
                    recipeContainerTransform.GetComponent<RecipeContainerUIType>().SetRecipeTemplateIcon(recipeSO);
                }
            }
        }
    }
}

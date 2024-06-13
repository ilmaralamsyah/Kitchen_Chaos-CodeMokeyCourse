using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeContainerUIType : MonoBehaviour
{
    [SerializeField] private int totalIngredient;
    [SerializeField] private Image recipeIcon;
    [SerializeField] private Transform ingredientContainer;
    [SerializeField] private Transform ingredientBackground;
    


    public int GetRecipeContainerUIType()
    {
        return totalIngredient;
    }

    public void SetRecipeTemplateIcon(RecipeSO recipeSO)
    {
        recipeIcon.sprite = recipeSO.recipeSprite;


        foreach (Transform child in ingredientContainer)
        {
            if (child == ingredientContainer) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform ingredientTransform = Instantiate(ingredientBackground, ingredientContainer);
            ingredientTransform.gameObject.SetActive(true);
            ingredientTransform.GetComponent<IngredientBackground>().SetIngredientIcon(kitchenObjectSO.sprite);
        }
    }
}

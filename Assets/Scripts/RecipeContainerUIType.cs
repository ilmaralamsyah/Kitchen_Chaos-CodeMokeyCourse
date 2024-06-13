using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeContainerUIType : MonoBehaviour
{
    [SerializeField] private int totalIngredient;
    [SerializeField] private Image recipeIcon;
    


    public int GetRecipeContainerUIType()
    {
        return totalIngredient;
    }

    public void SetRecipeTemplateIcon(RecipeSO recipeSO)
    {
        recipeIcon.sprite = recipeSO.recipeSprite;


    }
}

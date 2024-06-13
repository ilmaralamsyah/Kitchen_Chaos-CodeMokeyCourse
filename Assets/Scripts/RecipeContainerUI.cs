using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeContainerUI : MonoBehaviour
{
    [SerializeField] private List<RecipeContainerUIType> recipeContainerUIList;


    public List<RecipeContainerUIType> GetRecipeContainerUIList()
    {
        return recipeContainerUIList;
    }

}

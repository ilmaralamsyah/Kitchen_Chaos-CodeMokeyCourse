using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform ingredientIcon;


    private void Awake()
    {
        ingredientIcon.gameObject.SetActive(false);
    }

    public void SetRecipeText(RecipeSO recipeSO)
    {
        recipeText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == ingredientIcon) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(ingredientIcon, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}

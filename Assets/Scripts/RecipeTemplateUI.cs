using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplateUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform ingredientImage;
    [SerializeField] private DeliveryManagerUI deliveryManagerUI;

    private void Awake()
    {
        ingredientImage.gameObject.SetActive(false);
    }

    public void SetRecipeText(RecipeSO recipeSO)
    {
        recipeText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == ingredientImage) continue;
            Destroy(child.gameObject);
        }


        foreach (KitchenObjectSO ingredient in recipeSO.kitchenObjectSOList)
        {
            Transform ingredientTransform = Instantiate(ingredientImage, iconContainer);
            ingredientTransform.gameObject.SetActive(true);
            ingredientTransform.GetComponent<Image>().sprite = ingredient.sprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBackground : MonoBehaviour
{
    [SerializeField] private Image ingredientIcon;

    public void SetIngredientIcon(Sprite sprite)
    {
        this.GetComponent<IngredientBackground>().enabled = true;
        this.GetComponent<Image>().enabled = true;
        ingredientIcon.enabled = true;
        ingredientIcon.sprite = sprite;
    }
}

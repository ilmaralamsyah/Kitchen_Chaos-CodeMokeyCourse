using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform cointainer;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;

        UpdaeVisual();
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdaeVisual();
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdaeVisual();
    }
    
    private void UpdaeVisual()
    {
        foreach(Transform child in cointainer)
        {
            if(child == recipeTemplate) { continue; }
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, cointainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<RecipeTemplateUI>().SetRecipeText(recipeSO);
        }
    }
}

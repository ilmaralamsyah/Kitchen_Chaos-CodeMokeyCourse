using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenObjectIcon : MonoBehaviour
{

    [SerializeField] private KitchenObject kitchenObject;
    [SerializeField] private Image kitchenObjectIcon;

    private void Awake()
    {
        kitchenObjectIcon.sprite = kitchenObject.GetKitchenObjectSO().sprite;
    }
}

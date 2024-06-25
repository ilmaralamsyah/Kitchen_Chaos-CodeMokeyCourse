using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCheckerVisual : MonoBehaviour
{

    [SerializeField] private GameObject successBoard;
    [SerializeField] private GameObject failedBoard;


    private void Start()
    {
        DeliveryManager.Instance.OnOrderRecipeSuccess += DeliveryManager_OnOrderRecipeSuccess;
        DeliveryManager.Instance.OnOrderRecipeFailed += DeliveryManager_OnOrderRecipeFailed;
    }

    private void DeliveryManager_OnOrderRecipeFailed(object sender, System.EventArgs e)
    {
        StartCoroutine(ShowHideVisual(failedBoard));
    }

    private void DeliveryManager_OnOrderRecipeSuccess(object sender, System.EventArgs e)
    {
        StartCoroutine(ShowHideVisual(successBoard));
    }

    IEnumerator ShowHideVisual(GameObject board)
    {
        board.SetActive(true);
        yield return new WaitForSeconds(1);
        board.SetActive(false);
    }
}

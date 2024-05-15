using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO (){
        return kitchenObjectSO;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Already has an item");
        }

        this.clearCounter = clearCounter;
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }


}

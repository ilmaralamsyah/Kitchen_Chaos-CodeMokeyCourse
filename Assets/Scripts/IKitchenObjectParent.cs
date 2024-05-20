using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public Transform GetNewPositionFollow();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}

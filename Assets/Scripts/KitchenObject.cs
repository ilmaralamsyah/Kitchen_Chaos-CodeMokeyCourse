using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    private float fryingTimeElapsed;
    private float burningTimeElapsed;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public IKitchenObjectParent GetClearCounter()
    {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        if (kitchenObjectParent.HasKitchenObject())
        {

        }

        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetNewPositionFollow();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }



    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }


    public void SetKitchenObjectFryingProgress(float fryingTimeElapsed)
    {
        this.fryingTimeElapsed = fryingTimeElapsed;

    }

    public void SetKitchenObjectBurningProgress(float burningTimeElapsed)
    {
        this.burningTimeElapsed = burningTimeElapsed;
    }
    

    public float GetKitchenObjectFryingProgress()
    {
        return fryingTimeElapsed;
    }

    public float GetKitchenObjectBurningProgress()
    {
        return burningTimeElapsed;
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}

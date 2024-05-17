using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

    public event EventHandler OnPickedUpItem;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    

    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(player);
            OnPickedUpItem?.Invoke(this, EventArgs.Empty);
        }
    }

}

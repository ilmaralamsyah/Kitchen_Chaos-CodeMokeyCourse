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
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnPickedUpItem?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else if(HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

}

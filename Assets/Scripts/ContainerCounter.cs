using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerPickupObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {

        if (!player.HasKitchenObject())
        {
            //player isn't holding kitchen object
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerPickupObject?.Invoke(this, EventArgs.Empty);
        }
    }
}

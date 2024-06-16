using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnAnyTrashedItem;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnAnyTrashedItem?.Invoke(OnAnyTrashedItem, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
    }
}

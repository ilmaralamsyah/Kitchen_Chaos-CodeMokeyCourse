using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnAnyTrashedItem;
    public static event EventHandler OnPlateTrashed;

    new public static void ResetStaticData()
    {
        OnAnyTrashedItem = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject) || player.GetKitchenObject().TryGetDirtyPlate(out DirtyPlateKitchenObject dirtyPlateKitchenObject))
            {
                OnPlateTrashed?.Invoke(this, EventArgs.Empty);
            }
            player.GetKitchenObject().DestroySelf();
            OnAnyTrashedItem?.Invoke(this, EventArgs.Empty);
        }
    }
}

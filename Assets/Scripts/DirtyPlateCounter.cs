using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyPlateCounter : BaseCounter
{

    public event EventHandler OnSpawnedDirtyPlate;
    public event EventHandler OnPickedUpDirtyPlate;

    [SerializeField] private KitchenObjectSO dirtyPlateKitchenObjectSO;

    private int dirtyPlateAmountSpawned = 0;
    private float dirtyPlateTimer;
    private float dirtyPlateTimerMax = 10f;
    private int waitingDirtyPlate;

    private void Start()
    {
        DeliveryManager.Instance.OnOrderRecipeSuccess += DeliveryManager_OnOrderRecipeSuccess;
        DeliveryManager.Instance.OnOrderRecipeFailed += DeliveryManager_OnOrderRecipeFailed;
    }

    private void DeliveryManager_OnOrderRecipeFailed(object sender, EventArgs e)
    {
        waitingDirtyPlate++;
    }

    private void DeliveryManager_OnOrderRecipeSuccess(object sender, EventArgs e)
    {
        waitingDirtyPlate++;
    }

    private void Update()
    {
        if (waitingDirtyPlate > 0)
        {
            dirtyPlateTimer += Time.deltaTime;
            if (dirtyPlateTimer > dirtyPlateTimerMax)
            {
                dirtyPlateAmountSpawned++;
                waitingDirtyPlate--;
                dirtyPlateTimer = 0f;
                OnSpawnedDirtyPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (dirtyPlateAmountSpawned > 0)
            {
                KitchenObject.SpawnKitchenObject(dirtyPlateKitchenObjectSO, player);

                dirtyPlateAmountSpawned--;

                OnPickedUpDirtyPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
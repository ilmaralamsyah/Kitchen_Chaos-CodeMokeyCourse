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
    private bool isSpawning = false;

    private void Start()
    {
        DeliveryManager.Instance.OnOrderRecipeSuccess += DeliveryManager_OnOrderRecipeSuccess;
    }

    private void DeliveryManager_OnOrderRecipeSuccess(object sender, EventArgs e)
    {
        isSpawning = true;
    }

    private void Update()
    {
        if (isSpawning)
        {
            dirtyPlateTimer += Time.deltaTime;
            if (dirtyPlateTimer > dirtyPlateTimerMax)
            {
                dirtyPlateAmountSpawned++;
                isSpawning = false;
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
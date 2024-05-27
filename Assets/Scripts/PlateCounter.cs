using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{

    public event EventHandler OnSpawnedPlate;
    public event EventHandler OnPickedUpPlate;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private int plateAmountSpawned;
    private int plateAmountSpawnedMax = 4;
    private float plateTimer;
    private float plateTimerMax = 2f;

    private void Update()
    {
        plateTimer += Time.deltaTime;

        if(plateTimer > plateTimerMax)
        {
            if(plateAmountSpawned < plateAmountSpawnedMax)
            {
                plateAmountSpawned++;
                plateTimer = 0f;
                OnSpawnedPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(plateAmountSpawned > 0)
            {
                if (plateAmountSpawned == plateAmountSpawnedMax) { plateTimer = 0f; }

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                
                plateAmountSpawned--;

                OnPickedUpPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

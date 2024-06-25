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

    private void Start()
    {
        TrashCounter.OnPlateTrashed += TrashCounter_OnPlateTrashed;
    }

    private void TrashCounter_OnPlateTrashed(object sender, EventArgs e)
    {
        plateAmountSpawned--;
    }

    private void Update()
    {
        if (plateAmountSpawned < plateAmountSpawnedMax)
        {
            plateAmountSpawned++;
            OnSpawnedPlate?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateAmountSpawned > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPickedUpPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    // Start is called before the first frame update

    public event EventHandler OnSpawnedPlate;
    public event EventHandler OnPickedUpPlate;

    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    private float spawnTimer;
    private float spawnTimeMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnTimeMax)
        {
            spawnTimer = 0;

            if(plateSpawnedAmount < plateSpawnedAmountMax )
            {
                plateSpawnedAmount++;
                OnSpawnedPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(plateSpawnedAmount > 0)
            {
                plateSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPickedUpPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }


}

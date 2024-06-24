using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCounter : BaseCounter
{

    public static SinkCounter Instance { get; private set; }

    public event EventHandler OnCleanedPlate;
    public event EventHandler OnPickedUpCleanPlate;

    private enum State
    {
        Idle,
        Cleaning,
        Cleaned,
        PlateSpawned
    }

    [SerializeField] private Transform cleanedPlateTransform;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private State state;
    private float cleaningPlateTimer;
    private float cleaningPlateTimerMax = 5f;
    private int plateCleaned;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        state = State.Idle;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == null)
        {
            state = State.Idle;
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && plateCleaned > 0)
        {
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

            plateCleaned--;

            OnPickedUpCleanPlate?.Invoke(this, EventArgs.Empty);
        }
        else if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetDirtyPlate(out DirtyPlateKitchenObject dirtyPlateKitchenObject))
                {
                    state = State.Cleaning;
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else
        {
            cleaningPlateTimer = GetKitchenObject().GetKitchenObjectCleaningPlateProgress();
            state = State.Cleaning;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Cleaning:
                cleaningPlateTimer += Time.deltaTime;
                if (cleaningPlateTimer > cleaningPlateTimerMax)
                {
                    cleaningPlateTimer = 0;
                    Debug.Log(GetKitchenObject());
                    GetKitchenObject().DestroySelf();
                    state = State.Cleaned;
                }
                break;
            case State.Cleaned:
                plateCleaned++;
                OnCleanedPlate?.Invoke(this, EventArgs.Empty);
                state = State.Idle;
                break;
        }
        Debug.Log(state);
    }
}

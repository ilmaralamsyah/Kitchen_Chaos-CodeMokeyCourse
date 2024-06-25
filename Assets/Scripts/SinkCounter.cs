using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCounter : BaseCounter, IHasProgress
{

    public static SinkCounter Instance { get; private set; }

    public event EventHandler OnCleanedPlate;
    public event EventHandler OnPickedUpCleanPlate;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State currentState;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressChanged;
    }

    public enum State
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
            if(state == State.Cleaning) { return; }
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
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        currentState = State.Cleaning
                    });
                    player.GetKitchenObject().gameObject.SetActive(false);
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else
        {
            cleaningPlateTimer = GetKitchenObject().GetKitchenObjectCleaningPlateProgress();
            state = State.Cleaning;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                currentState = State.Cleaning
            });
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Cleaning:

                
                cleaningPlateTimer = GetKitchenObject().GetKitchenObjectCleaningPlateProgress();

                cleaningPlateTimer += Time.deltaTime;

                GetKitchenObject().SetKitchenObjectCleaningPlateProgress(cleaningPlateTimer);

                

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressChanged = cleaningPlateTimer / cleaningPlateTimerMax
                });

                if (cleaningPlateTimer > cleaningPlateTimerMax)
                {
                    cleaningPlateTimer = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressChanged = 0f
                    });
                    GetKitchenObject().DestroySelf();
                    state = State.Cleaned;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        currentState = State.Cleaned
                    });
                }
                break;
            case State.Cleaned:
                plateCleaned++;
                OnCleanedPlate?.Invoke(this, EventArgs.Empty);
                state = State.Idle;
                break;
        }
    }
}

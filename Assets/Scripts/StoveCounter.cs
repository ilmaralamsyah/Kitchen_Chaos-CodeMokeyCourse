using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnFryingEventArgs> OnFrying;
    public class OnFryingEventArgs : EventArgs
    {
        public State stateChanged;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressChanged;
    } 

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public State state;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    private float fryingTimer;
    private float burningTimer;
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        OnFrying?.Invoke(this, new OnFryingEventArgs
        {
            stateChanged = State.Idle,
        });
    }
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                if (HasKitchenObject())
                {
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressChanged = fryingTimer / fryingRecipeSO.maxFryingTime
                    });

                    if (fryingTimer >= fryingRecipeSO.maxFryingTime)
                    {

                        GetKitchenObject().DestroySelf();
                        
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        fryingTimer = 0f;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressChanged = 0f
                        });

                        state = State.Fried;

                        OnFrying?.Invoke(this, new OnFryingEventArgs
                        {
                            stateChanged = State.Fried,
                        });

                        burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    }
                }
                break;

            case State.Fried:

                if (HasKitchenObject())
                {
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressChanged = burningTimer / burningRecipeSO.maxBurningTime
                    });

                    
                    if (burningTimer >= burningRecipeSO.maxBurningTime)
                    {
                        GetKitchenObject().DestroySelf();
                        
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        burningTimer = 0f;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressChanged = 0f
                        });

                        state = State.Burned;

                        OnFrying?.Invoke(this, new OnFryingEventArgs
                        {
                            stateChanged = State.Burned,
                        });
                    }
                }
                break;
            case State.Burned:
                break;
        }
        Debug.Log(state);
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                
                if(fryingRecipeSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO()))
                {
                    fryingTimer = 0f;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressChanged = 0f
                    });

                    state = State.Frying;

                    OnFrying?.Invoke(this, new OnFryingEventArgs
                    {
                        stateChanged = State.Frying,
                    });
                } 
                else if (burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO()))
                {
                    burningTimer = 0f;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressChanged = 0f
                    });

                    state = State.Fried;

                    OnFrying?.Invoke(this, new OnFryingEventArgs
                    {
                        stateChanged = State.Fried
                    });
                }
                
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnFrying?.Invoke(this, new OnFryingEventArgs
                {
                    stateChanged = State.Idle,
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressChanged = 0f
                });
            }
        }
    }

    private bool HasInputWithRcipe(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetOutputFromInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetOutputFromInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        return null;
    }

    private FryingRecipeSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}

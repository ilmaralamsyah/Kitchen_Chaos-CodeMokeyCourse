using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IHasProgress;

public class StoveCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnFryingObjecttEventArgs> OnFryingObjectt;
    public class OnFryingObjecttEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] BurningRecipeSO[] burningRecipeSOArray;


    public State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;


    private void Start()
    {
        state = State.Idle;

        OnFryingObjectt?.Invoke(this, new OnFryingObjecttEventArgs{
            state = state
        });;

        
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:

                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.maxFryingTime
                    });
                    if (fryingTimer > fryingRecipeSO.maxFryingTime)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnFryingObjectt?.Invoke(this, new OnFryingObjecttEventArgs
                        {
                            state = state
                        });

                        
                    }
                    break;
                case State.Fried:
                    if (HasKitchenObject())
                    {
                        burningTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = burningTimer / burningRecipeSO.maxBurningTime
                        });

                        if (burningTimer > burningRecipeSO.maxBurningTime)
                        {
                            //fried

                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                            state = State.Burned;

                            OnFryingObjectt?.Invoke(this, new OnFryingObjecttEventArgs
                            {
                                state = state
                            });

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                    }
                    break;
                case State.Burned:

                    break;
            }

        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //no kitchen object in counter
            if (player.HasKitchenObject())
            {
                //player holding kitchen object
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;

                    fryingTimer = 0f;

                    OnFryingObjectt?.Invoke(this, new OnFryingObjecttEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.maxFryingTime
                    });
                }
            }
            else
            {
                //player isn't holding kitchen object
            }
        }
        else
        {
            //there's kitchen object in counter
            if (player.HasKitchenObject())
            {
                //player holding kitchen object

            }
            else
            {
                //player isn't holding kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnFryingObjectt?.Invoke(this, new OnFryingObjecttEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
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


    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
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

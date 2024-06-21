using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public event EventHandler <IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    public event EventHandler OnCut;

    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    [SerializeField] private CuttingRecipeSO[]cuttingRecipeSOArray;


    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasInputWithRcipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    CuttingRecipeSO cuttingRecipeSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

                    cuttingProgress = 0;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressChanged = (float)cuttingProgress / cuttingRecipeSO.maxCuttingStep
                    });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressChanged = 0f
                });
            }
        }
    }


    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasInputWithRcipe(GetKitchenObject().GetKitchenObjectSO()))
        {
            if(!player.HasKitchenObject())
            {
                cuttingProgress++;

                CuttingRecipeSO cuttingRecipeSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressChanged = (float)cuttingProgress / cuttingRecipeSO.maxCuttingStep
                });

                OnCut?.Invoke(this, EventArgs.Empty);
                //OnCut2?.Invoke(); // using action
                OnAnyCut.Invoke(this, EventArgs.Empty);

                if (cuttingProgress >= cuttingRecipeSO.maxCuttingStep)
                {
                    KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }
    }

    private bool HasInputWithRcipe(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetOutputFromInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetOutputFromInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }

    private CuttingRecipeSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

}

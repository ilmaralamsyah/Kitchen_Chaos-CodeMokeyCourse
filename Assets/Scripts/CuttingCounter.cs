using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    public event EventHandler <OnCuttingProgressChangedArgs> OnCuttingProgressChanged;
    public class OnCuttingProgressChangedArgs : EventArgs {  
        public float progressNormalized; 
    }
    public event EventHandler OnCut;

    //public Action OnCut2; using action

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

                    OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCuttingStep
                    });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
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
                OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCuttingStep
                });

                OnCut?.Invoke(this, EventArgs.Empty);
                //OnCut2?.Invoke(); // using action

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

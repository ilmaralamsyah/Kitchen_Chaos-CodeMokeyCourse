using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBarFlashingAnimation : MonoBehaviour
{
    private const string IS_BURNING = "IsBurning";

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        animator.SetBool(IS_BURNING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        bool showWarningBar = e.progressChanged >= 0.5f && stoveCounter.IsFried();
        animator.SetBool(IS_BURNING, showWarningBar);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimator : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
        //example using action below
        //cuttingCounter.OnCut2 += OnCut2;
    }

    /*private void OnCut2()
    {
        animator.SetTrigger(CUT);
    }*/

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}

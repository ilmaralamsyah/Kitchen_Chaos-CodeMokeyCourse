using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particleOnGameObject;

    

    private const string CUT = "Cut";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnFryingObjectt += StoveCounter_OnFryingObjectt;
    }

    private void StoveCounter_OnFryingObjectt(object sender, StoveCounter.OnFryingObjecttEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particleOnGameObject.SetActive(showVisual);
    }
}

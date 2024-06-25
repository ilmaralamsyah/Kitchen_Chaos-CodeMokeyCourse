using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveFX;
    [SerializeField] private GameObject stoveParticle;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnFrying;
    }

    private void StoveCounter_OnFrying(object sender, StoveCounter.OnFryingEventArgs e)
    {
        bool isStateChanged = e.stateChanged == StoveCounter.State.Frying || e.stateChanged == StoveCounter.State.Fried;
        stoveFX.SetActive(isStateChanged);
        stoveParticle.SetActive(isStateChanged);
    }
}

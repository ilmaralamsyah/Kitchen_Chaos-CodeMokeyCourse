using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveFX;
    [SerializeField] GameObject stoveParticle;

    private void Start()
    {
        stoveCounter.OnFrying += StoveCounter_OnFrying;

}

    private void StoveCounter_OnFrying(object sender, StoveCounter.OnFryingEventArgs e)
    {
        bool isFrying = e.stateChanged == StoveCounter.State.Frying || e.stateChanged == StoveCounter.State.Fried;
        stoveFX.SetActive(isFrying);
        stoveParticle.SetActive(isFrying);
    }
}

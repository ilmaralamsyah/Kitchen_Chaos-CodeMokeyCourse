using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCounterVisual : MonoBehaviour
{
    [SerializeField] private SinkCounter sinkCounter;
    [SerializeField] private GameObject cleaningVFX;


    private void Start()
    {
        sinkCounter.OnStateChanged += SinkCounter_OnStateChanged;
    }

    private void SinkCounter_OnStateChanged(object sender, SinkCounter.OnStateChangedEventArgs e)
    {
        bool isStateChanged = e.currentState == SinkCounter.State.Cleaning;
        cleaningVFX.SetActive(isStateChanged);
    }
        
}

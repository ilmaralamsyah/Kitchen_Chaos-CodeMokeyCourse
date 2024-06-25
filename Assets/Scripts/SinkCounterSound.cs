using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCounterSound : MonoBehaviour
{
    [SerializeField] private SinkCounter sinkCounter;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sinkCounter.OnStateChanged += SinkCounter_OnStateChanged;
    }

    private void SinkCounter_OnStateChanged(object sender, SinkCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.currentState == SinkCounter.State.Cleaning;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}

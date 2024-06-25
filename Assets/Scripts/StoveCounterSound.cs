using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        playWarningSound = stoveCounter.IsFried() && e.progressChanged >= 0.5f;

    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnFryingEventArgs e)
    {
        bool playSound = e.stateChanged == StoveCounter.State.Frying || e.stateChanged == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float waringSoundTimerMax = 0.2f;
                warningSoundTimer = waringSoundTimerMax;

                SoundManager.Instance.PlayWarningSFX(stoveCounter.transform.position);
            }
        }
    }
}

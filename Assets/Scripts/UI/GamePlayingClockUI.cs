using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{

    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private AudioClip timeUpAudioClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.OnTimeLeftAlert += GameManager_OnTimeLeftAlert;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(timeUpAudioClip);
    }

    private void GameManager_OnTimeLeftAlert(object sender, System.EventArgs e)
    {
        audioSource.Play();
    }

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
        timerText.text = GameManager.Instance.GetGamePlayingTimerFormatted();
    }
}

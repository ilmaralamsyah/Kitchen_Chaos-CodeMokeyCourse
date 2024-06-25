using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{

    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;


    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
        timerText.text = GameManager.Instance.GetGamePlayingTimerFormatted();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Button backButton;

    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;

        backButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        musicVolumeSlider.value = MusicManager.Instance.GetMusicVolume();
        soundFXVolumeSlider.value = SoundManager.Instance.GetSoundEffectVolume();

        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        isGamePaused = false;
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        isGamePaused = true;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if(!isGamePaused) { return; }
        SoundManager.Instance.ChangeVolume(soundFXVolumeSlider.value);
        MusicManager.Instance.ChangeVolume(musicVolumeSlider.value);
    }
}

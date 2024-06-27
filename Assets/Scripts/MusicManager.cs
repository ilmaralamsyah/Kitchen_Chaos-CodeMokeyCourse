using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{


    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance {  get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
    }

    private void Instance_OnGameOver(object sender, System.EventArgs e)
    {
        audioSource.Stop();
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return audioSource.volume;
    }
}

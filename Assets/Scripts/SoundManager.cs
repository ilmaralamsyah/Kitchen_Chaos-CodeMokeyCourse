using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance { get; private set; }

    private const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectVolume";

    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;

    private float volume;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnOrderRecipeSuccess += DeliveryManager_OnOrderRecipeSuccess;
        DeliveryManager.Instance.OnOrderRecipeFailed += DeliveryManager_OnOrderRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnAnyKitchenObjectDropped += BaseCounter_OnKitchenObjectDropped;
        Player.Instance.OnAnyKitchenObjectPickedUp += Player_OnKitchenObjectPickedUp;
        TrashCounter.OnAnyTrashedItem += TrashCounter_OnTrashedItem;
        //PlayerSound.OnPlayerWalking += PlayerSound_OnPlayerWalking;
    }

    private void PlayerSound_OnPlayerWalking(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefsSO.footstep, Player.Instance.transform.position);
    }

    private void TrashCounter_OnTrashedItem(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipsRefsSO.objectDrop, trashCounter.transform.position);
    }

    private void BaseCounter_OnKitchenObjectDropped(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipsRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnKitchenObjectPickedUp(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnOrderRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnOrderRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClip, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClip[Random.Range(0, audioClip.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootstepSFX(Vector3 position, float volume)
    {
        PlaySound(audioClipsRefsSO.footstep, position, volume);
    }

    public void ChangeVolume(float volume)
    {
        this.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetSoundEffectVolume()
    {
        return volume;
    }
}

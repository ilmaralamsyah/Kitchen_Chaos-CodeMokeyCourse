using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private float footstepTimer;
    private float footstepTimerMax = 0.1f;

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking())
            {
                float footstepVolume = 1f;
                SoundManager.Instance.PlayFootstepSFX(player.transform.position, footstepVolume);
                //OnPlayerWalking?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

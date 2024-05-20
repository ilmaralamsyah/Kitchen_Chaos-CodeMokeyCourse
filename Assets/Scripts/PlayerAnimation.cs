using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    Animator animator;
    Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}

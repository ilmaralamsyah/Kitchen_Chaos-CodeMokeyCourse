using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounterVisual : MonoBehaviour
{

    [SerializeField] private ContainerCounter containerCounter;

    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerPickupObject += ContainerCounter_OnPlayerPickupObject;
    }

    private void ContainerCounter_OnPlayerPickupObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}

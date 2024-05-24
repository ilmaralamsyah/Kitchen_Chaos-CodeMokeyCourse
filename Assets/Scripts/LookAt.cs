using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    private enum Mode
    {
        LookAt,
        LookAtInverterd,
        LookAtForward,
        LookAtForwardInverted
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverterd:
                Vector3 dirToCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirToCamera);
                break;
            case Mode.LookAtForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.LookAtForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}

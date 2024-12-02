using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{

    public CinemachineVirtualCamera cutsceneCamera;
    public CinemachineVirtualCamera playerCamera;

    public void ChangeToPlayerCamera()
    {
        cutsceneCamera.Priority = 1;
        playerCamera.Priority = 2;
    }
}

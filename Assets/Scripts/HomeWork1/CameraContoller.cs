using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
    
    private Queue<CinemachineVirtualCamera> camerasQueue;

    public bool IsVerticalCamera { get; private set; }

    private void Awake()
    {
        camerasQueue = new(cameras);
        SwitchNextCamera();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchNextCamera();
        }
    }

    private void SwitchNextCamera()
    {
        CinemachineVirtualCamera nextCamera = camerasQueue.Dequeue();

        foreach (var camera in cameras)
            camera.gameObject.SetActive(false);

        nextCamera.gameObject.SetActive(true);
        IsVerticalCamera = nextCamera.GetComponent<CameraSettings>().IsVerticalCamera;

        camerasQueue.Enqueue(nextCamera);
    }
}

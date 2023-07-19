using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cameras;
    private int _currentCameraIndex;
    [SerializeField] private GameObject _cockpitObject;

    private void Start()
    {
        // Make sure there is at least one camera and set its priority to the highest
        if (_cameras.Length > 0)
        {
            SetCameraPriority(0, 15);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchToNextCamera();
        }
    }

    private void SwitchToNextCamera()
    {
        // Make sure there is more than one camera
        if (_cameras.Length > 1)
        {
            // Reset the priority of the current camera
            SetCameraPriority(_currentCameraIndex, 10);

            // Increment the camera index and wrap around if it exceeds the array length
            _currentCameraIndex = (_currentCameraIndex + 1) % _cameras.Length;

            // Set the priority of the new camera
            SetCameraPriority(_currentCameraIndex, 15);

            // Toggle cockpit based on the current camera
            ToggleCockpit(_currentCameraIndex == 0);
        }
    }

    private void SetCameraPriority(int cameraIndex, int priority)
    {
        var camera = _cameras[cameraIndex];

        if (camera.GetComponent<CinemachineVirtualCamera>())
        {
            camera.GetComponent<CinemachineVirtualCamera>().Priority = priority;
        }

        if (camera.GetComponent<CinemachineBlendListCamera>())
        {
            camera.GetComponent<CinemachineBlendListCamera>().Priority = priority;
        }
    }

    private void ToggleCockpit(bool enable)
    {
        _cockpitObject.SetActive(enable);
    }
}
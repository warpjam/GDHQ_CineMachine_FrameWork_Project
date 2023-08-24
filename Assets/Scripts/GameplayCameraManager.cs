using Cinemachine;
using UnityEngine;

public class GameplayCameraManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cameras;
    private int _currentCameraIndex;
    [SerializeField] private GameObject _cockpitObject;

    private void Start()
    {
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
        if (_cameras.Length > 1)
        {
            SetCameraPriority(_currentCameraIndex, 10);
            _currentCameraIndex = (_currentCameraIndex + 1) % _cameras.Length;
            SetCameraPriority(_currentCameraIndex, 15);
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
    }

    private void ToggleCockpit(bool enable)
    {
        _cockpitObject.SetActive(enable);
    }
}
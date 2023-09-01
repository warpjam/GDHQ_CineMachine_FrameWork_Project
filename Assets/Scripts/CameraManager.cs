using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cameras;
    [SerializeField] private int _currentCamera;
    [SerializeField] private GameObject _cockpitConstruct;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            _currentCamera++;
            if (_currentCamera >= _cameras.Length)
            {
                _currentCamera = 0;
            }
            SetLowCamPriorities();
            SetCurrentCamera();
            ToggleCockpit();
            
        }
        
        
    }

    public void SetLowCamPriorities()
    {
        foreach (var c in _cameras)
        {
            if (c.GetComponent<CinemachineVirtualCamera>())
            {
                c.GetComponent<CinemachineVirtualCamera>().Priority = 10;
            }

            if (c.GetComponent<CinemachineBlendListCamera>())
            {
                c.GetComponent<CinemachineBlendListCamera>().Priority = 10;
            }
            
        }
    }

    public void SetCurrentCamera()
    {
        if (_cameras[_currentCamera].GetComponent<CinemachineVirtualCamera>())
        {
            _cameras[_currentCamera].GetComponent<CinemachineVirtualCamera>().Priority = 15;
        }

        if (_cameras[_currentCamera].GetComponent<CinemachineBlendListCamera>())
        {
            _cameras[_currentCamera].GetComponent<CinemachineBlendListCamera>().Priority = 15;
        }
    }
    
    private void ToggleCockpit()
    {
        _cockpitConstruct.SetActive(!_cockpitConstruct.activeSelf);
    }

    /*private void CheckIdle()
    {
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0 && !Input.anyKey)
        {
            _idleTimer += Time.deltaTime;

            if (_idleTimer >= _idleTimeTrigger)
            {
                _isIdle = true;
                Debug.Log("No Input detected, start external camera cycle");
                StartCameraCycle()
                    ;

            }
        }
        else
        {
            _idleTimer = 0f; // Reset the idle timer when input is detected
            if (_isIdle)
            {
                _isIdle = false;
                StopCameraCycle();

            }
        }
    }*/

}
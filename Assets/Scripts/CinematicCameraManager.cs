using System;
using Cinemachine;
using UnityEngine;
using System.Collections;

public class CinematicCameraManager : MonoBehaviour
{
    [SerializeField] private float _cameraCycleTime = 5f; // Time to stay on each camera

    [SerializeField] private CinemachineVirtualCamera[] _pauseCams;
    private int _currentPauseCameraIndex;

    private Coroutine _cameraCycleCoroutine; // Reference to the coroutine
    private bool _isCyclingCameras = false; // Flag to control the cycling
    
    [SerializeField] private bool _isIdle = false;
    [SerializeField] private float _idleTimer = 0;
    [SerializeField] private float _idleTimeTrigger = 5f;

    private void Start()
    {
        _pauseCams = GetComponentsInChildren<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckIdle();
    }
    
    private void CheckIdle()
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
    }


    public void StartCameraCycle()
    {
        _isCyclingCameras = true; // Set flag to indicate camera cycling
        StartCoroutine(CyclePauseCamerasCoroutine());
    }

    public void StopCameraCycle()
    {
        _isCyclingCameras = false; // Reset flag when stopping camera cycling
        StopCoroutine(CyclePauseCamerasCoroutine());
        // Reset the priorities of pause cameras when stopping the cycle
        for (int i = 0; i < _pauseCams.Length; i++)
        {
            SetCameraPriority(i, 10);
        }
    }

    IEnumerator CyclePauseCamerasCoroutine()
    {
        while (_isCyclingCameras)
        {
            SwitchToNextPauseCamera();
            yield return new WaitForSeconds(_cameraCycleTime);
        }
    }

    private void SwitchToNextPauseCamera()
    {
        if (_pauseCams.Length > 0)
        {
            SetCameraPriority(_currentPauseCameraIndex, 10);
            _currentPauseCameraIndex = (_currentPauseCameraIndex + 1) % _pauseCams.Length;
            SetCameraPriority(_currentPauseCameraIndex, 15);
        }
    }

    private void SetCameraPriority(int cameraIndex, int priority)
    {
        if (cameraIndex >= 0 && cameraIndex < _pauseCams.Length)
        {
            _pauseCams[cameraIndex].Priority = priority;
        }
    }
}
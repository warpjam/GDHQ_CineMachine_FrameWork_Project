using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    #region old variables
    public Transform target = null;
    public Transform rig = null;

    public float distance = 10f;
    public float rotationSpeed = 10f;

    Vector3 cameraPosition;
    Vector3 smoothPosition;
    float smoothTime = 0.05f; //1.125f;
    float angle;

    private int camType = 0;
    #endregion

    public Transform deadCam; // EMPTY OBJECT IN SCENE

    void Update()
    {
        if(Input.GetKeyDown("c")) 
        {
            camType += 1;
            if(camType >= 3)
            {
                camType = 0;
            }
        }
    // }

    // void FixedUpdate() 
    // {
        if(camType == 0 && rig != null) RigCam(rig);
        else if(camType == 1 && target != null) TopCam();
        else if(camType == 2 && target != null) BackCam();
        else RigCam(deadCam); // REUSE RigCam
    }

    void RigCam(Transform rigTransform)
    {
        transform.position = rigTransform.position;
        transform.rotation = rigTransform.rotation;
    }

    void TopCam()
    {
        transform.position = target.position + target.up * distance + target.forward * (distance / 3);
        transform.rotation = target.rotation * Quaternion.Euler(90f,0,0);
    }

    void BackCam()
    {
        // Calculate Position
        // cameraPosition = target.position - (target.forward * distance) + target.up * distance * 0.25f;
        cameraPosition = target.position + target.up * distance + target.forward * (distance / 3);
        smoothPosition = Vector3.Lerp(transform.position, cameraPosition, smoothTime * Time.deltaTime);
        transform.position = smoothPosition;

        // Calculate Rotation
        transform.rotation = target.rotation * Quaternion.Euler(90f,0,0);
        // angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target.rotation));
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, (rotationSpeed + angle) * Time.deltaTime);
    }

    void BackCamOriginal()
    {
        // Calculate Position
        cameraPosition = target.position - (target.forward * distance) + target.up * distance * 0.25f;
        smoothPosition = Vector3.Lerp(transform.position, cameraPosition, smoothTime * Time.deltaTime);
        transform.position = smoothPosition;

        // Calculate Rotation
        angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target.rotation));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, (rotationSpeed + angle) * Time.deltaTime);
    }
}

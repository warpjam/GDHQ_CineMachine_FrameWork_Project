using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateXYZ : MonoBehaviour
{
    public float xRot = 0f;
    public float yRot = 0f;
    public float zRot = 15f;
    public bool constant = false;

    public bool sinusoidal = false;
    public float frequency = 1;
    public float magnitude = -5;

    void Update()
    {
        if(Input.GetKeyUp("space") && !constant)
        {
            transform.Rotate(xRot, yRot, zRot, Space.Self);
        } 
        if(constant)
        {
            transform.Rotate(xRot * Time.deltaTime, yRot * Time.deltaTime, zRot * Time.deltaTime, Space.World);
        }

        if(sinusoidal)
        {
            transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude; 
        }
    }
}

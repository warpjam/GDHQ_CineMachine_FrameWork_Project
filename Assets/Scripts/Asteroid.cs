using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float _rotationSpeed = 10f; // Adjust the speed of rotation as needed

    private void Update()
    {
        // Rotate the object around its own axis
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
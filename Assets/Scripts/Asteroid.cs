using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotationSpeed = 10f; // Adjust the speed of rotation as needed

    private void Update()
    {
        // Rotate the object around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodySpaceVehicle : MonoBehaviour
{
    public float torque = 100f;
    public float thrust = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate() 
    {
        float roll = Input.GetAxis("Horizontal");
        float pitch = Input.GetAxis("Vertical");
        bool throttle = Input.GetKey("space");

        rb.AddRelativeTorque(Vector3.back * torque * roll);
        rb.AddRelativeTorque(Vector3.right * torque * pitch);
        if(throttle) 
        {
            rb.AddRelativeForce(Vector3.forward * thrust);
        }
    }
}
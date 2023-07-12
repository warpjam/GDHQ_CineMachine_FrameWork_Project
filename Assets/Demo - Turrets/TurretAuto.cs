using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAuto : MonoBehaviour
{
    public fireProjectile weapon;
    public Transform target;
    public Transform barrel;

    private void Update() 
    {
        Aim();
        weapon.fire();
    }

    private void Aim()
    {
        // TURN
        float targetPlaneAngle = vector3AngleOnPlane(target.position, transform.position, -transform.up, transform.forward);
        Vector3 newRotation = new Vector3(0, targetPlaneAngle, 0);
        transform.Rotate(newRotation, Space.Self);
        
        // UP/DOWN
        float upAngle = Vector3.Angle(target.position, barrel.transform.up);
        Vector3 upRotation = new Vector3(-upAngle + 90, 0, 0);
        barrel.transform.Rotate(upRotation, Space.Self);
    }

    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

        return projectedVectorAngle;
    } 
}

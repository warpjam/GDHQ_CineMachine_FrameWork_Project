using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShip : MonoBehaviour
{
    #region Variables
    public float torque = 500f;
    public float thrust = 5000f;
    private Rigidbody rb;
    public Transform player;
    public EmitProjectile weapon;
    public GameObject explosion;
    public ParticleSystem explosionPart;
    #endregion

    #region New Variables
    public float health = 500f;

    public float maxHealth;
    public Canvas UI;
    public Image healthUI;
    #endregion

    #region Monobehavior Methods
    void Start()
    {
        GameObject playerOb = GameObject.Find("WaveForm AP");
        player = playerOb.GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        maxHealth = health; // SET MAX HEALTH TO CURRENT. 
    }

    void FixedUpdate() 
    {
        Fly();
        //Cheat();
        //UI.transform.LookAt(player);
        UI.transform.rotation = player.rotation;
    }
    #endregion

    #region Custom Methods
    void Fly()
    {
        Vector3 targetDir = player.position - transform.position;
        
        float xyAngle = vector3AngleOnPlane(player.position, transform.position, transform.forward, transform.up);
        float yzAngle = vector3AngleOnPlane(player.position, transform.position, transform.right, transform.forward);
        
        if(Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
        {
            rb.AddRelativeTorque(Vector3.forward * -torque * (xyAngle/Mathf.Abs(xyAngle)));
        }
        else if(yzAngle >= 1f)
        {
            rb.AddRelativeTorque(Vector3.right * -torque);

            weapon.fire();
        }
        
        rb.AddRelativeForce(Vector3.forward * thrust);
    }

    void Cheat()
    {
        transform.LookAt(player);
        weapon.fire();
        rb.AddRelativeForce(Vector3.forward * thrust);
    }

    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

        return projectedVectorAngle;
    } 
    
    void TestCode()
    {
        // TEST THIS CODE *****************************************************************************************************
        Transform target = player;
        // PROJECT TO LOCAL XY PLANE
        Vector3 projectedVector = Vector3.ProjectOnPlane(target.position - player.position, transform.forward);
        // CALCULATE ANGLE
        float xyAngle = Vector3.SignedAngle(projectedVector, transform.up, transform.forward);
        // 
        // float xzAngle = vector3AngleOnPlane(player.position, transform.position, transform.up * -1, transform.forward);
                //rb.AddRelativeForce(Vector3.forward * Mathf.Clamp((distance - 10)/50, 0f, 1f) * thrust);
        //                 Vector3 targetLocation = player.position - transform.position;
        // float distance = targetLocation.magnitude;
        Vector3 from = target.position - transform.position;
        Vector3 to = transform.up; // .forward, .right
        Vector3.SignedAngle(from, to, transform.forward); // "left" of up is negative
    }
    #endregion
    
    public void damage (float damageAmount)
    {
        health -= damageAmount;
        healthUI.fillAmount = health/maxHealth; // SHRINK THE HEALTH BAR WHEN DAMAGED

        if(health <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(explosionPart, transform.position, Quaternion.identity);  
            Destroy(gameObject);
        }
    }
}
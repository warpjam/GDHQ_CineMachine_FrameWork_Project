using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitProjectile : MonoBehaviour
{
    public GameObject projectile;

    [SerializeField]
    private float fireRatePerSecond = 1f;

    private float timeBetweenShots;
    private float nextFire;
    public laserBeams laserBeamProperties;

    void Start()
    {
        timeBetweenShots = 1f / fireRatePerSecond;
        nextFire = Time.time + timeBetweenShots;
    }

    public void fire()
    {
        if(Time.time >= nextFire)
        {
            nextFire = Time.time + timeBetweenShots;
            GameObject.Instantiate(projectile, transform.position, transform.rotation);
        }
    }
    
}

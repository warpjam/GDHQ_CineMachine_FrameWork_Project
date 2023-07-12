using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectile : MonoBehaviour
{
    public GameObject projectile;
    public AudioEvent audioSound;

    [SerializeField]
    private float fireRatePerSecond = 1f;

    private float timeBetweenShots;
    private float nextFire;
    private AudioSource audioSource;


    void Start()
    {
        timeBetweenShots = 1f / fireRatePerSecond;
        nextFire = Time.time + timeBetweenShots;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            fire();
        }
    }

    public void fire()
    {
        if(Time.time >= nextFire)
        {
            nextFire = Time.time + timeBetweenShots;
            GameObject.Instantiate(projectile, transform.position, transform.rotation);
            audioSound.PlayOneShot((audioSource));
        }     
    }
}
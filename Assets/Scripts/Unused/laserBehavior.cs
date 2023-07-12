using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    private float endOfLife;
    private Rigidbody rb;

    void Start()
    {
        endOfLife = Time.time + (150f / speed);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        if(Time.time > endOfLife) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player") Destroy(gameObject);
        if(collision.gameObject.tag != "Indestructible"  && collision.gameObject.tag != "Player") Destroy(collision.gameObject);     
    }
}
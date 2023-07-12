using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tfLaserBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    private float endOfLife;

    void Start()
    {
        endOfLife = Time.time + (150f / speed);
    }

    void Update()
    {
        if(Time.time > endOfLife) Destroy(gameObject);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player") Destroy(gameObject);
        if(collision.gameObject.tag != "Indestructible" && collision.gameObject.tag != "Player") Destroy(collision.gameObject);  
    }
}
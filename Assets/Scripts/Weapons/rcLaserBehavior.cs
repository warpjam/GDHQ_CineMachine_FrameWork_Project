using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rcLaserBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    private float endOfLife;
    private float force = 1000f;

    void Start()
    {
        endOfLife = Time.time + (150f / speed);
    }

    void Update()
    {
        if(Time.time > endOfLife) Destroy(gameObject);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Time.deltaTime * speed))
        {
            Destroy(gameObject);
            //hit.rigidbody.AddForceAtPosition(force * speed * transform.forward, hit.point);
            EnemyShip enemy = hit.collider.GetComponent<EnemyShip>();
            if(enemy) enemy.damage(100f);

            RigidBodyAircraft player = hit.collider.GetComponent<RigidBodyAircraft>();
            if(player) player.damage(10f);
        }
        else transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}

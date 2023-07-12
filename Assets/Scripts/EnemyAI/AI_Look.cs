using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Look : MonoBehaviour
{
    public Transform player;
    public float thrust;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(player);

        Vector3 targetLocation = player.position - transform.position;
        float distance = targetLocation.magnitude;
        rb.AddRelativeForce(Vector3.forward * Mathf.Clamp((distance - 10)/50, 0f, 1f) * thrust);
    }
}

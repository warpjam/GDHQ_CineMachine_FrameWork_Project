using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource audioSource;
    private float endOfLife;
    public AudioEvent audioSound;

    void Start()
    {
        endOfLife = Time.time + 5f;
        audioSource = GetComponent<AudioSource>();
        // audioSound.PlayOneShot((audioSource));
        audioSource.Play();
    }

    void Update()
    {
        if(Time.time > endOfLife) Destroy(gameObject);
    }
}

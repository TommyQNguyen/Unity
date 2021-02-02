using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{

    public AudioSource AudioSource;

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.Play();
        Destroy(gameObject);
    }
}

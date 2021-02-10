using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float DestroyTimer = 1;

    // Update is called once per frame
    void Update()
    {
        // Destroys the gameObject(Explosion) after 1 second after rendering
        Destroy(gameObject, 1);
    }
}

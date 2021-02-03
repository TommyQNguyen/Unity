using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DestroyTimer = 5;
    public float Speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.right indique ou regarde actuellement l'objet, 
        // donc on avance dans cette direction de la vitesse * le temps
        transform.position = transform.position + transform.right * Speed * Time.deltaTime;

        DestroyTimer = DestroyTimer - Time.deltaTime;
        if (DestroyTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    // Also add Capsule Collider 2D for the items that will collide
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }


}

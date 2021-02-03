using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletSpawnPoint;

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var bulletRotation = transform.rotation * Quaternion.Euler(0, 0, 30);

            Instantiate(Bullet, BulletSpawnPoint.position, bulletRotation);
        }

        
    }
}

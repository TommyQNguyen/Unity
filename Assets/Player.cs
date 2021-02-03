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
            var bulletRotation = transform.rotation * Quaternion.Euler(0, 0, 0);

            Instantiate(Bullet, BulletSpawnPoint.position, bulletRotation);
        }

        if (Input.GetButtonDown("Fire2"))
        {

            var bulletRotation1 = transform.rotation * Quaternion.Euler(0, 0, 0);       // Se dirige tout droit
            var bulletRotation2 = transform.rotation * Quaternion.Euler(330, 0, -330);  // Se dirige en diagonale en haut
            var bulletRotation3 = transform.rotation * Quaternion.Euler(30, 0, -30);    // Se dirige en diagonale en bas

            // Fait apparaitre les Bullets a partir de leur spawn point
            Instantiate(Bullet, BulletSpawnPoint.position, bulletRotation1);
            Instantiate(Bullet, BulletSpawnPoint.position, bulletRotation2);
            Instantiate(Bullet, BulletSpawnPoint.position, bulletRotation3);
        }


    }

    
}

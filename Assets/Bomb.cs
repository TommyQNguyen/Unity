using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float BombTimer = 1;
    public float BombExplodeTimer = 2;
    public GameObject BulletGameObject;
    public GameObject ExplosionGameObject;

    public Flash BombFlash { get; private set; }
    void Awake()
    {
        BombFlash = GetComponent<Flash>();
        Invoke("OnFlash", BombTimer);
        Invoke("OnExplode", BombExplodeTimer);
    }

    void OnFlash()
    {
        BombFlash.StartFlash();
    }

    void OnExplode()
    {
        Destroy(gameObject);
        Instantiate(ExplosionGameObject, transform.position, Quaternion.identity);

        var bulletRotation1 = transform.rotation * Quaternion.Euler(0, 0, 0);
        var bulletRotation2 = transform.rotation * Quaternion.Euler(45, 0, -45);
        var bulletRotation3 = transform.rotation * Quaternion.Euler(90, 0, -90);
        var bulletRotation4 = transform.rotation * Quaternion.Euler(135, 0, -135);
        var bulletRotation5 = transform.rotation * Quaternion.Euler(180, 0, -180);
        var bulletRotation6 = transform.rotation * Quaternion.Euler(225, 0, -225);
        var bulletRotation7 = transform.rotation * Quaternion.Euler(270, 0, -270);
        var bulletRotation8 = transform.rotation * Quaternion.Euler(315, 0, -315);

        // Shoot les bullets partout sur explosion
        Instantiate(BulletGameObject, transform.position, bulletRotation1);
        Instantiate(BulletGameObject, transform.position, bulletRotation2);
        Instantiate(BulletGameObject, transform.position, bulletRotation3);
        Instantiate(BulletGameObject, transform.position, bulletRotation4);
        Instantiate(BulletGameObject, transform.position, bulletRotation5);
        Instantiate(BulletGameObject, transform.position, bulletRotation6);
        Instantiate(BulletGameObject, transform.position, bulletRotation7);
        Instantiate(BulletGameObject, transform.position, bulletRotation8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

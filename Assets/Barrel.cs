using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float BarrelTimer = 2;
    public GameObject Bullet; // Drag my Explosion prefab in Unity
    public GameObject Explosion; // Drag my Explosion prefab in Unity
    public Health Health { get; private set; }
    public Flash Flash { get; private set; }

    //public AudioClip BarrelExplodesSound;

    // Start is called before the first frame update
    void Awake()
    {
        Health = GetComponent<Health>(); // Chercher l'instance dans le GameObject
        Flash = GetComponent<Flash>();

        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        Destroy(gameObject);
        GameManager.Instance.PrefabManager.Spawn(PrefabManager.Global.Explosion, transform.position, Quaternion.identity);
        GameManager.Instance.SoundManager.Play(SoundManager.Sfx.Explosion);

        var bulletRotation1 = transform.rotation * Quaternion.Euler(0, 0, 0);
        var bulletRotation2 = transform.rotation * Quaternion.Euler(45, 0, -45);
        var bulletRotation3 = transform.rotation * Quaternion.Euler(90, 0, -90);
        var bulletRotation4 = transform.rotation * Quaternion.Euler(135, 0, -135);
        var bulletRotation5 = transform.rotation * Quaternion.Euler(180, 0, -180);
        var bulletRotation6 = transform.rotation * Quaternion.Euler(225, 0, -225);
        var bulletRotation7 = transform.rotation * Quaternion.Euler(270, 0, -270);
        var bulletRotation8 = transform.rotation * Quaternion.Euler(315, 0, -315);

        // Shoot les bullets partout sur explosion
        Instantiate(Bullet, transform.position, bulletRotation1);
        Instantiate(Bullet, transform.position, bulletRotation2);
        Instantiate(Bullet, transform.position, bulletRotation3);
        Instantiate(Bullet, transform.position, bulletRotation4);
        Instantiate(Bullet, transform.position, bulletRotation5);
        Instantiate(Bullet, transform.position, bulletRotation6);
        Instantiate(Bullet, transform.position, bulletRotation7);
        Instantiate(Bullet, transform.position, bulletRotation8);
    }

    // Update is called once per frame
    void Update()
    {
        if (Flash.enabled)
        {
            BarrelTimer = BarrelTimer - Time.deltaTime;
            if (BarrelTimer < 0)
            {
                //Debug.Log("Barrel Timer: " + BarrelTimer);
                Destroy(gameObject);
                Instantiate(Explosion, transform.position, Quaternion.identity);

                // A le rendre plus optimal quand j'aurai fini les autres taches... 
                var bulletRotation1 = transform.rotation * Quaternion.Euler(0, 0, 0);       
                var bulletRotation2 = transform.rotation * Quaternion.Euler(45, 0, -45);  
                var bulletRotation3 = transform.rotation * Quaternion.Euler(90, 0, -90);    
                var bulletRotation4 = transform.rotation * Quaternion.Euler(135, 0, -135);       
                var bulletRotation5 = transform.rotation * Quaternion.Euler(180, 0, -180);  
                var bulletRotation6 = transform.rotation * Quaternion.Euler(225, 0, -225);    
                var bulletRotation7 = transform.rotation * Quaternion.Euler(270, 0, -270);       
                var bulletRotation8 = transform.rotation * Quaternion.Euler(315, 0, -315);  

                // Shoot les bullets partout sur explosion
                Instantiate(Bullet, transform.position, bulletRotation1);
                Instantiate(Bullet, transform.position, bulletRotation2);
                Instantiate(Bullet, transform.position, bulletRotation3);
                Instantiate(Bullet, transform.position, bulletRotation4);
                Instantiate(Bullet, transform.position, bulletRotation5);
                Instantiate(Bullet, transform.position, bulletRotation6);
                Instantiate(Bullet, transform.position, bulletRotation7);
                Instantiate(Bullet, transform.position, bulletRotation8);
            }
        }
    }

    public void onBulletCollision(Bullet bullet)
    {
        // Should only the player have a health component? because monster and spawner and barrels are made in more than 1 unlike player
        Health.Value -= 1;


        if (Health.Value > 0)
        {
            Flash.StartFlash();
        }

        //Debug.Log("Barrel is hit by Bullet");
        Debug.Log("Barrel Health left: " + Health.Value);
    }
}

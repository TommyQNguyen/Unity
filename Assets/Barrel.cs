using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    public Health Health { get; private set; }
    public Flash Flash { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Health = GetComponent<Health>(); // Chercher l'instance dans le GameObject
        Flash = GetComponent<Flash>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBulletCollision(Bullet bullet)
    {
        // Should only the player have a health component? because monster and spawner and barrels are made in more than 1 unlike player
        //Health.BarrelHealthQuantity = Health.BarrelHealthQuantity - 1;

        //if (Health.BarrelHealthQuantity > 0)
        //{
        //    Flash.StartFlash();
        //}

        //if (Health.BarrelHealthQuantity < 1)
        //{
        //    Debug.Log("Enemy is dead");
        //    Destroy(gameObject);
        //}

        Debug.Log("Barrel is hit by Bullet");
    }
}

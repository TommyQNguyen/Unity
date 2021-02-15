using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Monster;
    public float InstantiateTimer = 5;
    public Health Health { get; private set; }


    public void Awake()
    {
        Health = GetComponent<Health>();
        InvokeRepeating("SpawnMonster", InstantiateTimer, InstantiateTimer);
    }

    void SpawnMonster()
    {
        Debug.Log("Spawning monster");
        Instantiate(Monster, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBulletCollision(Bullet bullet)
    {
        Health.SpawnerHealthQuantity = Health.SpawnerHealthQuantity - 1;

        if (Health.SpawnerHealthQuantity < 1)
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }

        Debug.Log("Spawner Health: " + Health.SpawnerHealthQuantity);
    }
}

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
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InstantiateTimer = InstantiateTimer - Time.deltaTime;
        if (InstantiateTimer < 0)
        {
            Debug.Log("Spawning monster");
            // Creer une explosion, a sa transform.position?, 
            // Quaternion.identity: https://docs.unity3d.com/ScriptReference/Quaternion-identity.html
            Instantiate(Monster, transform.position, Quaternion.identity);
            InstantiateTimer = 5;
        }
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

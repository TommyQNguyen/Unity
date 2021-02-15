using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Player Player;
    public GameObject Monster;
    public float InstantiateTimer = 5;
    public Health Health { get; private set; }
    public AudioClip SpawnerExplodesSound;
    public AudioClip SpawnerSound;

    public void Awake()
    {
        Player = FindObjectOfType<Player>();
        Health = GetComponent<Health>();
        InvokeRepeating("SpawnMonster", InstantiateTimer, InstantiateTimer);
    }

    void SpawnMonster()
    {
        Debug.Log("Spawning monster");
        Instantiate(Monster, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(SpawnerSound, transform.position, 1.0f);
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
            Player.Score.ScoreNumber = Player.Score.ScoreNumber + 500;
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(SpawnerExplodesSound, transform.position, 1.0f);
        }
        //Debug.Log("Spawner Health: " + Health.SpawnerHealthQuantity);
    }
}
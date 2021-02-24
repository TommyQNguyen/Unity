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

        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        Destroy(gameObject);
        Player.Score.ScoreNumber = Player.Score.ScoreNumber + 500;
        GameManager.Instance.SoundManager.Play(SoundManager.ShooterSfx.Explosion);
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
        Health.Value = Health.Value - 1;

        //if (Health.Value < 1)
        //{
        //Player.Score.ScoreNumber = Player.Score.ScoreNumber + 500;
        //Destroy(gameObject);
        //AudioSource.PlayClipAtPoint(SpawnerExplodesSound, transform.position, 1.0f);
        //}
        Debug.Log("Spawner Health: " + Health.Value);
    }
}
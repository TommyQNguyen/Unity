﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player Player;
    public float DestroyTimer = 5;
    public float Speed = 5;
    public GameObject Explosion; // Drag my Explosion prefab in Unity

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player>();
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
    void OnTriggerEnter2D(Collider2D collision2D)
    {

        // Creer une explosion, a sa transform.position?, 
        // Quaternion.identity: https://docs.unity3d.com/ScriptReference/Quaternion-identity.html

        Debug.Log("Bullet : On trigger");

         // Teacher version:
         var monster = collision2D.gameObject.GetComponent<Monster>();
         if (monster != null)
         {
            monster.onBulletCollision(this);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Player.Score.ScoreNumber = Player.Score.ScoreNumber + 25;
            //Destroy(gameObject); // C'est probablement repetitif etant donne que 
            // la methode .onBulletCollision le fait deja
            GameManager.Instance.SoundManager.Play(SoundManager.ShooterSfx.Hit);
        }

        var spawner = collision2D.gameObject.GetComponent<Spawner>();
        if (spawner != null)
        {
            spawner.onBulletCollision(this);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Player.Score.ScoreNumber = Player.Score.ScoreNumber + 25;

            GameManager.Instance.SoundManager.Play(SoundManager.ShooterSfx.Hit);
        }


        var barrel = collision2D.gameObject.GetComponent<Barrel>();
        if (barrel != null)
        {
            barrel.onBulletCollision(this);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            //Destroy(gameObject);

            GameManager.Instance.SoundManager.Play(SoundManager.ShooterSfx.Hit);
        }
    }
}

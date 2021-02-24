using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Player player;
    public float Speed = 10;
    public int Health = 3;
    public float ScoreOnKill;

    private GameObject Player;

    private void Start()
    {
        //Player player = FindObjectOfType<Player>();
        //player.gameObject
        Player = FindObjectOfType<Player>().gameObject;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            Player.transform.position, 
            Speed * Time.deltaTime);
        //gameObject
        // A changer 3e argument, pour que le monstre regarde vers le joueur
        // transform.LookAt(Player.transform.position, Vector3);
        transform.right = Player.transform.position - transform.position;
    }

    public void onBulletCollision(Bullet bullet)
    {
        Health = Health - 1;

        if (Health < 1)
        {
            player.Score.ScoreNumber = player.Score.ScoreNumber + ScoreOnKill;
            //Debug.Log("Enemy is dead");
            Destroy(gameObject);

            GameManager.Instance.SoundManager.Play(SoundManager.ShooterSfx.Explosion);
        }

        //Debug.Log("Enemy Health: " + Health);
    }
}

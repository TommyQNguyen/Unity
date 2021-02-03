using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject Player;
    public float Speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            Player.transform.position, 
            Speed * Time.deltaTime);

        // A changer 3e argument, pour que le monstre regarde vers le joueur
        // transform.LookAt(Player.transform.position, Vector3);
    }
}

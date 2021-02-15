using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null && player.Items.BombQuantity < 3)
        {
            player.Items.BombQuantity = player.Items.BombQuantity + 1;
            Destroy(gameObject);
            //Debug.Log("Bomb Amount: " + player.Items.BombQuantity);
        }
        else
        {
            Debug.Log("Bombs already maximum");
        }
    }
}

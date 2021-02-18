using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        
        if (player != null && player.Health.Value < 5)
        {
            player.Health.Value = player.Health.Value + 1;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Health already maximum");
        }
    }
}

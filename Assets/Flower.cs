using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
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
        var mario = collision.GetComponent<Mario>();
        var health = collision.GetComponentInParent<Health>();

        if (mario && health.Value < 2 && mario.CurrentState == Mario.State.Small)
        {
            Destroy(gameObject);
            mario.CurrentState = Mario.State.Fire;
            health.Value += 1;
            GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Item);
            Debug.Log("Mario current Health: " + health.Value);
        }

    }
}

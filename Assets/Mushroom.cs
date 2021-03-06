using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public PlatformController PlatformController { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnWall += OnWall;
    }

    private void OnWall(PlatformController platformController)
    {
        PlatformController.FacingController.Flip();
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mario = collision.GetComponent<Mario>();
        var health = collision.GetComponentInParent<Health>();

        if (mario && health.Value < 2)
        {
            Destroy(gameObject);
            mario.CurrentState = Mario.State.Big;
            health.Value += 1;
            GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Item);
            Debug.Log("Mario current Health: " + health.Value);
        }
        
    }
}

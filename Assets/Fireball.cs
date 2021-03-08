using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public PlatformController PlatformController { get; private set; }

    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnWall += OnWall;
    }

    private void OnWall(PlatformController platformController)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;
        PlatformController.InputJump = true;

        var speedRatio = PlatformController.CurrentSpeed / PlatformController.MoveSpeed;
        //Animator.speed = RunAnimationSpeed.Lerp(speedRatio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mario = collision.GetComponent<Mario>();
        var health = collision.GetComponentInParent<Health>();

        if (health && health.CanBeDamaged && !mario)
        {
            health.Value -= 1;
            Destroy(gameObject);
            GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Kick);
        }
    }
}

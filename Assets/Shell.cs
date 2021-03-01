using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Vector2 SpinAnimationSpeed;
    public PlatformController PlatformController { get; private set; }
    public Animator Animator { get; private set; }

    private bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnWall += OnWall;

        Animator = GetComponent<Animator>();
    }

    private void OnWall(PlatformController platformController)
    {
        PlatformController.FacingController.Flip();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            PlatformController.InputMove = PlatformController.FacingController.Direction;
            Animator.Play("Shell_Spin");

            var speedRatio = PlatformController.CurrentSpeed / PlatformController.MoveSpeed;
            Animator.speed = SpinAnimationSpeed.Lerp(speedRatio);
        }
        else if (isActivated == false)
        {
            Animator.Play("Shell_Idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mario = collision.GetComponent<Mario>();
        var health = collision.GetComponentInParent<Health>();

        if (mario && isActivated == false)
        {
            isActivated = true;
            GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Kick);
        }
        else if (mario && isActivated)
        {
            var shellPosition = PlatformController.BoxCollider2D.bounds.min.y + 0.5 * PlatformController.BoxCollider2D.bounds.extents.y ;
            var marioPosition = collision.bounds.min.y ;

            if (marioPosition > shellPosition)
            {
                isActivated = false;
                mario.PlatformController.Jump();
                GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Kick);
            }
            else
            {
                health.Value -= 1;
            }
        }
        else if (health && health.CanBeDamaged && isActivated && !mario)
        {
            health.Value -= health.Value;
            GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Kick);
        }
    }
}

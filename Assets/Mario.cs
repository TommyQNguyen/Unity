using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mario : MonoBehaviour
{
    public Vector2 RunAnimationSpeed;

    public PlatformController PlatformController { get; private set; }
    
    public Animator Animator { get; private set; }
    public Health Health { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnJump += OnJump;
        PlatformController.OnFall += OnFall;
        PlatformController.OnMoveStart += OnMoveStart;
        PlatformController.OnMoveStop += OnMoveStop;
        PlatformController.OnLand += OnLand;

        Animator = GetComponent<Animator>();
        Health = GetComponent<Health>();
        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        

        // A faire
        // Animator.Play("Mario_Dead");

        // Attendre 3 secondes
        // Empecher bouger
        //PlatformController.enabled = false;
        //PlatformController.BoxCollider2D.enabled = false;
        //PlatformController.Rigidbody2D.simulated = false; // Decocher le RigidBody

        Destroy(gameObject);


        // Restart level
        GameManager.Instance.Invoke(nameof(GameManager.RestartLevel), 3.0f);
        
    }

    

    private void OnFall(PlatformController platformController)
    {
        Animator.Play("Mario_Jump");
    }

    private void OnLand(PlatformController platformController)
    {
        if (PlatformController.IsMoving)
        {
            Animator.Play("Mario_Run");
        }
        else
        {
            Animator.Play("Mario_Idle");
        }
    }

    private void OnMoveStop(PlatformController platformController)
    {
        if (PlatformController.IsGrounded)
        {
            Animator.Play("Mario_Idle");
        }

    }

    private void OnMoveStart(PlatformController platformController)
    {
        if (PlatformController.IsGrounded)
        {
            Animator.Play("Mario_Run");
        }
    }

    private void OnJump(PlatformController platformController)
    {
        Animator.Play("Mario_Jump");
        GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Jump);
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputJump = Input.GetButtonDown("Jump");
        PlatformController.InputMove = Input.GetAxisRaw("Horizontal");

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Mario_Run"))
        {
            var speedRatio = Mathf.Abs(PlatformController.CurrentSpeed / PlatformController.MoveSpeed);
            Animator.speed = RunAnimationSpeed.Lerp(speedRatio);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerStay2D: " + collision.gameObject.name);

        if (collision.gameObject.name == "SpikesHitbox")
        {
            Health.Value -= 1;
        }


        var health = collision.GetComponentInParent<Health>();
        if (health)
        {
            Debug.Log("OnTriggerStay2D Health");

            // Mario ou Goomba gagne
            var marioPosition = PlatformController.BoxCollider2D.bounds.min.y;
            var enemyPosition = collision.bounds.min.y + 0.5 * collision.bounds.extents.y;

            if (marioPosition > enemyPosition)
            {
                // Mario gagne
                health.Value -= 1;

                PlatformController.Jump();
                GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Stomp);
            }
            else
            {
                // Goomba gagne
                Health.Value -= 1;
            }
        }






    }




}

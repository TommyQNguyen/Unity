using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mario : MonoBehaviour
{
    public enum State
    {
        Small,
        Big,
        Fire
    }

    public enum Animation
    {
        Idle,
        Run,
        Jump,
        Dead
    }

    private State _currentState;

    public State CurrentState
    {
        get { return _currentState; }
        set 
        {
            _currentState = value;



            // A faire:
            // Dead = 0
            // Small = 1 point de vie

            // Big
            // Fire = 2 points de vie

            // Eventuellement creer  le OnHit pour gerer
            switch (CurrentState)
            {
                case State.Small:
                    Health.Value = 1;
                    break;
                case State.Big:
                    Health.Value = 2;
                    break;
                case State.Fire:
                    Health.Value = 2;
                    break;
            }


            UpdateAnimation();
        }
    }

    private Animation _currentAnimation;
    public Animation CurrentAnimation
    {
        get { return _currentAnimation; }
        set
        {
            _currentAnimation = value;
            UpdateAnimation();
        }
    }

    private void UpdateAnimation()
    {
        var animationName = AnimationName;
        Animator.Play(animationName);
    }

    public string AnimationName
    {
        get
        {
            if (CurrentAnimation == Animation.Dead)
                return "Mario_Small_Dead";

            var prefix = CurrentState.ToString();
            var suffix = CurrentAnimation.ToString();

            return "Mario_" + prefix + "_" + suffix;
        }
    }

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
        Health.OnHit += OnHit;
        Health.OnDeath += OnDeath;

        GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerMusic.Music);
    }

    private void OnHit(Health health)
    {
        if (CurrentState == State.Big || CurrentState == State.Fire)
        {
            CurrentState = State.Small;
        }
    }

    private void Start()
    {
        CurrentState = State.Small;
        OnLevelRestart();
    }

    private void OnDeath(Health health)
    {
        CurrentAnimation = Animation.Dead;

        PlatformController.enabled = false;
        PlatformController.BoxCollider2D.enabled = false;
        PlatformController.Rigidbody2D.simulated = false; // Decocher le RigidBody

        // Restart level
        // Attendre 3 secondes
        // Empecher bouger
        GameManager.Instance.Invoke(nameof(GameManager.RestartLevel), 3.0f);
    }

    
    private void OnFall(PlatformController platformController)
    {
        CurrentAnimation = Animation.Jump;
    }

    private void OnLand(PlatformController platformController)
    {
        if (PlatformController.IsMoving)
        {
            CurrentAnimation = Animation.Run;
        }
        else
        {
            CurrentAnimation = Animation.Idle;
        }
    }

    private void OnMoveStop(PlatformController platformController)
    {
        if (PlatformController.IsGrounded)
        {
            CurrentAnimation = Animation.Idle;
        }
    }

    private void OnMoveStart(PlatformController platformController)
    {
        if (PlatformController.IsGrounded)
        {
            CurrentAnimation = Animation.Run;
        }
    }

    private void OnJump(PlatformController platformController)
    {
        CurrentAnimation = Animation.Jump;
        GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Jump);
    }

    public void OnLevelStart(LevelEntrance levelEntrance)
    {
        if (levelEntrance != null)
        {
            transform.position = levelEntrance.transform.position;
        }
        else
        {
            transform.position = Vector3.zero;
        }

        PlatformController.Reset();
    }
    public void OnLevelRestart()
    {
        CurrentState = State.Small;
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputJump |= Input.GetButtonDown("Jump");
        PlatformController.InputMove = Input.GetAxisRaw("Horizontal");

        if (CurrentAnimation == Animation.Run)
        {
            var speedRatio = Mathf.Abs(PlatformController.CurrentSpeed / PlatformController.MoveSpeed);
            Animator.speed = RunAnimationSpeed.Lerp(speedRatio);
        }
        else
        {
            Animator.speed = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentState = State.Small;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentState = State.Big;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentState = State.Fire;
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (CurrentState == State.Fire)
            {
                var fireballGameObject = GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Fireball, transform.position);
                var fireball = fireballGameObject.GetComponent<Fireball>();

                var marioCurrentFacing = this.PlatformController.FacingController.Facing;
                var fireballCurrentFacing = fireball.PlatformController.FacingController.Facing;

                if (marioCurrentFacing != fireballCurrentFacing)
                {
                    fireball.PlatformController.FacingController.Flip();
                }
                GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Fireball);
            }   
        }
    }

    private void OnTrigger(Collider2D collision)
    {
        //Debug.Log("OnTrigger: " + collision.gameObject.name);

        
        // A changer pour mettre dans mon script Spike si j'ai le temps
        if (collision.gameObject.name == "SpikesHitbox")
        {
            Health.Value -= 1;
            GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Hit);
        }

        var health = collision.GetComponentInParent<Health>();
        if (health && health.CanBeDamaged)
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
                if (Health.CanBeDamaged)
                { 
                    Health.Value -= 1;
                    Debug.Log("Enemy hit Mario, current Mario HP: " + Health.Value);
                    GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Hit);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTrigger(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger(collision);
    }
}

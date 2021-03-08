using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Vector2 RunAnimationSpeed;
    public PlatformController PlatformController { get; private set; }
    public Health Health { get; private set; }
    public Animator Animator { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnWall += OnWall;
        PlatformController.OnMoveStart += OnMoveStart;

        Health = GetComponent<Health>();
        Health.OnDeath += OnDeath;

        Animator = GetComponent<Animator>();
    }

    private void OnMoveStart(PlatformController platformController)
    {
        if (PlatformController.IsGrounded)
        {
            Animator.Play("Goomba_Walk");
        }
    }

    private void OnDeath(Health health)
    {
        GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Smoke, transform.position);
        Destroy(gameObject);
    }

    private void OnWall(PlatformController platformController)
    {
        PlatformController.FacingController.Flip();
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;

        var speedRatio = PlatformController.CurrentSpeed / PlatformController.MoveSpeed;
        Animator.speed = RunAnimationSpeed.Lerp(speedRatio);
    }
}

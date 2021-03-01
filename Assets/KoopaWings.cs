using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaWings : MonoBehaviour
{
    public Vector2 RunAnimationSpeed;
    public PlatformController PlatformController { get; private set; }
    public Health Health { get; private set; }
    public Animator Animator { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnJump += OnJump;
        PlatformController.OnLand += OnLand;

        Health = GetComponent<Health>();
        Health.OnHit += OnHit;
        Health.OnDeath += OnDeath;

        Animator = GetComponent<Animator>();
    }

    private void OnLand(PlatformController platformController)
    {
        Animator.Play("Koopa_Walk");
    }

    private void OnJump(PlatformController platformController)
    {
        Animator.Play("Koopa_Fly");
    }

    private void OnHit(Health health)
    {
        Health.Value -= 1;
        //Animator.Play("Koopa_Walk");
    }

    private void OnDeath(Health health)
    {
        Destroy(gameObject);
        GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Shell, transform.position);
    }

    private void Koopa_Fly()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;
        PlatformController.InputJump = true;

        var speedRatio = PlatformController.CurrentSpeed / PlatformController.MoveSpeed;
        Animator.speed = RunAnimationSpeed.Lerp(speedRatio);
    }

    private void Koopa_Walk()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;

        var speedRatio = PlatformController.CurrentSpeed / PlatformController.MoveSpeed;
        Animator.speed = RunAnimationSpeed.Lerp(speedRatio);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.Value >= 2)
        {
            Koopa_Fly();
        }
        else if (Health.Value < 2)
        {
            Koopa_Walk();
        }
    }
}

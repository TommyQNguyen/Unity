using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public Vector2 RunAnimationSpeed;

    public PlatformController PlatformController { get; private set; }
    
    public Animator Animator { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnJump += OnJump;
        PlatformController.OnMoveStart += OnMoveStart;
        PlatformController.OnMoveStop += OnMoveStop;
        PlatformController.OnLand += OnLand;
        Animator = GetComponent<Animator>();
    }

    private void OnLand(PlatformController platformController)
    {
        if (PlatformController.IsMoving)
        {
            Animator.Play("Mario_Idle");
        }
        else
        {
            Animator.Play("Mario_Idle");
        }
    }

    private void OnMoveStop(PlatformController platformController)
    {
        if (!PlatformController.IsJumping)
        {
            Animator.Play("Mario_Idle");
        }

    }

    private void OnMoveStart(PlatformController platformController)
    {
        if (!PlatformController.IsJumping)
        {
            Animator.Play("Mario_Run");
        }
    }

    private void OnJump(PlatformController platformController)
    {
        Animator.Play("Mario_Jump");
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
}

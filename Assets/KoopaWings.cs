using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaWings : MonoBehaviour
{
    public Vector2 RunAnimationSpeed;
    public PlatformController PlatformController { get; private set; }
    public Animator Animator { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnMoveStart += OnMoveStart;

        Animator = GetComponent<Animator>();
    }

    private void OnMoveStart(PlatformController platformController)
    {
        Animator.Play("Koopa_Fly");
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;
        PlatformController.InputJump = true;

        var speedRatio = PlatformController.CurrentSpeed / PlatformController.MoveSpeed;
        Animator.speed = RunAnimationSpeed.Lerp(speedRatio);
    }
}

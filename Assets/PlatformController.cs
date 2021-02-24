using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FacingController))]
public class PlatformController : MonoBehaviour
{
    private static readonly string[] LayerMaskNames = { "Floor" };
    private static readonly Vector2 ColliderSize = new Vector2(0.005f, 0.005f);
    private static readonly Color DebugCollisionOnColor = Color.green;
    private static readonly Color DebugCollisionOffColor = Color.red;

    public static bool ShowDebug { get; set; } = true;

    public delegate void PlatformControllerEvent(PlatformController platformController);

    public PlatformControllerEvent OnMoveStart;
    public PlatformControllerEvent OnMoveStop;
    public PlatformControllerEvent OnJump;
    public PlatformControllerEvent OnFall;
    public PlatformControllerEvent OnLand;
    public PlatformControllerEvent OnWall;
    public PlatformControllerEvent OnCeiling;
    public PlatformControllerEvent OnJumpsRemainingChanged;

    public float MoveSpeed = 1;
    public float MoveAcceleration = 1000;
    public float MoveDeceleration = 1000;
    public float AirMoveSpeedMultiplier = 1.0f;
    public float JumpStrength = 200;
    public int Jumps = 1;

    private int _layerMask;

    public bool InputJump { get; set; } // true ButtonDown
    public float InputMove { get; set; } // -1, 1 AxisRaw

    public BoxCollider2D BoxCollider2D { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    public FacingController FacingController { get; private set; }

    public bool IsGrounded { get; private set; }
    public bool IsWalled { get; private set; }
    public bool IsWalledLeft { get; private set; }
    public bool IsWalledRight { get; private set; }
    public bool IsCeiling { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsFalling { get; private set; }
    public bool IsMoving { get; private set; }

    public float CurrentSpeed { get { return Rigidbody2D.velocity.x; } }

    private float _jumpsRemaining;
    public float JumpsRemaining
    {
        get { return _jumpsRemaining; }
        set
        {
            var previous = _jumpsRemaining;
            _jumpsRemaining = Mathf.Clamp(value, 0, Jumps);

            if (_jumpsRemaining != previous)
                OnJumpsRemainingChanged?.Invoke(this);
        }
    }

    public void ResetJumpsRemaining()
    {
        JumpsRemaining = Jumps;
    }

    public void Jump()
    {
        // Cancel previous fall momentum on jump
        Rigidbody2D.velocity = Rigidbody2D.velocity.WithY(0);

        // Jump the same height regardless of object mass and gravity
        var jumpStrength = JumpStrength * Rigidbody2D.mass * Mathf.Sqrt(Rigidbody2D.gravityScale);
        Rigidbody2D.AddForce(Vector2.up * jumpStrength);

        JumpsRemaining -= 1;
        IsJumping = true;
        IsFalling = false;
        OnJump?.Invoke(this);
    }

    private void Awake()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        FacingController = GetComponent<FacingController>();

        _layerMask = LayerMask.GetMask(LayerMaskNames);

        ResetJumpsRemaining();
    }

    private void Update()
    {
        UpdateCollisions();
        UpdateJump();
        UpdateMove();
    }

    private void UpdateCollisions()
    {
        bool wasGrounded = IsGrounded;
        bool wasCeiling = IsCeiling;
        bool wasWalledLeft = IsWalledLeft;
        bool wasWalledRight = IsWalledRight;

        IsGrounded = false;
        IsCeiling = false;
        IsWalledLeft = false;
        IsWalledRight = false;

        var bounds = BoxCollider2D.bounds;
        var groundRaycastHit2D = Physics2D.BoxCastAll(bounds.center, bounds.size + new Vector3(ColliderSize.x, 0, 0), transform.localEulerAngles.z, Vector2.down, ColliderSize.y, _layerMask);
        var ceilingRaycastHit2D = Physics2D.BoxCastAll(bounds.center, bounds.size + new Vector3(ColliderSize.x, 0, 0), transform.localEulerAngles.z, Vector2.up, ColliderSize.y, _layerMask);
        var walledLeftRaycastHit2D = Physics2D.BoxCastAll(bounds.center, bounds.size, transform.localEulerAngles.z, Vector2.left, ColliderSize.x, _layerMask);
        var walledRightRaycastHit2D = Physics2D.BoxCastAll(bounds.center, bounds.size, transform.localEulerAngles.z, Vector2.right, ColliderSize.x, _layerMask);

        UpdateCollisionRaycastHits(groundRaycastHit2D);
        UpdateCollisionRaycastHits(ceilingRaycastHit2D);
        UpdateCollisionRaycastHits(walledLeftRaycastHit2D);
        UpdateCollisionRaycastHits(walledRightRaycastHit2D);

        IsWalled = IsWalledLeft || IsWalledRight;

        if (wasGrounded != IsGrounded
            && IsGrounded)
        {
            ResetJumpsRemaining();
            IsJumping = false;
            IsFalling = false;
            OnLand?.Invoke(this);
        }

        if (wasCeiling != IsCeiling
            && IsCeiling)
        {
            OnCeiling?.Invoke(this);
        }

        if ((wasWalledLeft != IsWalledLeft
            || wasWalledRight != IsWalledRight)
            && IsWalled)
        {
            OnWall?.Invoke(this);
        }

        if (!IsGrounded
            && !IsFalling
            && Rigidbody2D.velocity.y < 0.0f)
        {
            IsFalling = true;
            OnFall?.Invoke(this);
        }

        DebugDrawCollisions();
    }

    private void UpdateCollisionRaycastHits(RaycastHit2D[] raycastHits)
    {
        foreach (var raycastHit in raycastHits)
        {
            //Debug.Log(String.Format("Contact : GameObject {0} Point {1} Normal {2}",
            //    raycastHit.collider.name,
            //    raycastHit.point,
            //    raycastHit.normal));

            if (raycastHit.normal.y > 0.0f)
            {
                //Debug.Log("Ground");
                IsGrounded = true;
            }
            else if (raycastHit.normal.y < 0.0f)
            {
                //Debug.Log("Ceiling");
                IsCeiling = true;
            }

            if (raycastHit.normal.x > 0.0f)
            {
                //Debug.Log("Left");
                IsWalledLeft = true;
            }
            else if (raycastHit.normal.x < 0.0f)
            {
                //Debug.Log("Right");
                IsWalledRight = true;
            }
        }
    }

    private void UpdateJump()
    {
        if (!InputJump)
            return;

        InputJump = false;

        if (JumpsRemaining <= 0)
            return;

        if (IsGrounded
            || (Jumps > 1 && JumpsRemaining > 0))
        {
            Jump();
        }
    }

    private void UpdateMove()
    {
        if (InputMove != 0.0f)
            UpdateMoveAcceleration();
        else
            UpdateMoveDeceleration();

        // Started moving
        if (!IsMoving
            && Rigidbody2D.velocity.x != 0.0f)
        {
            IsMoving = true;
            OnMoveStart?.Invoke(this);
        }

        // Stopped moving
        if (IsMoving
            && Rigidbody2D.velocity.x == 0.0f)
        {
            IsMoving = false;
            OnMoveStop?.Invoke(this);
        }
    }

    private void UpdateMoveAcceleration()
    {
        var direction = new Vector3(InputMove, 0, 0);
        if (direction.magnitude > 1)
            direction = direction.normalized;

        var speedMultiplier = IsGrounded ? 1.0f : AirMoveSpeedMultiplier;
        var velocity = Rigidbody2D.velocity;
        velocity.x += direction.x * speedMultiplier * MoveAcceleration * Time.fixedDeltaTime;
        velocity.x = Mathf.Clamp(velocity.x, -MoveSpeed, MoveSpeed);
        Rigidbody2D.velocity = velocity;

        if (InputMove < 0.0f)
            FacingController.Facing = Facing.Left;
        else
            FacingController.Facing = Facing.Right;

        InputMove = 0.0f;
    }

    private void UpdateMoveDeceleration()
    {
        var velocity = Rigidbody2D.velocity;
        if (velocity.x > 0)
        {
            velocity.x -= MoveDeceleration * Time.fixedDeltaTime;
            if (velocity.x < 0)
                velocity.x = 0;
        }
        else
        {
            velocity.x += MoveDeceleration * Time.fixedDeltaTime;
            if (velocity.x > 0)
                velocity.x = 0;
        }

        Rigidbody2D.velocity = velocity;
    }

    private void DebugDrawCollisions()
    {
        if (!ShowDebug)
            return;

        // Ground
        {
            var bounds = BoxCollider2D.bounds;
            bounds.center = bounds.center.WithY(bounds.center.y - bounds.extents.y);
            bounds.extents = bounds.extents.WithY(ColliderSize.y / 2);
            DebugDrawBox(bounds, IsGrounded);
        }

        // Ceiling
        {
            var bounds = BoxCollider2D.bounds;
            bounds.center = bounds.center.WithY(bounds.center.y + bounds.extents.y);
            bounds.extents = bounds.extents.WithY(ColliderSize.y / 2);
            DebugDrawBox(bounds, IsCeiling);
        }

        // Left
        {
            var bounds = BoxCollider2D.bounds;
            bounds.center = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y);
            bounds.extents = new Vector2(ColliderSize.x / 2, bounds.extents.y);
            DebugDrawBox(bounds, IsWalledLeft);
        }

        // Right
        {
            var bounds = BoxCollider2D.bounds;
            bounds.center = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y);
            bounds.extents = new Vector2(ColliderSize.x / 2, bounds.extents.y);
            DebugDrawBox(bounds, IsWalledRight);
        }
    }

    private void DebugDrawBox(Bounds bounds, bool isOn)
    {
        if (!ShowDebug)
            return;

        var color = isOn ? DebugCollisionOnColor : DebugCollisionOffColor;
        DebugExtensions.DrawBox(bounds, color);
    }
}

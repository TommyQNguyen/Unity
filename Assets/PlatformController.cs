using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FacingController))]
public class PlatformController : MonoBehaviour
{
    private static readonly string[] LayerMaskNames = { "Floor" };
    private static readonly float GroundedColliderSize = 0.01f;
    private static readonly float WalledColliderSize = 0.01f;
    private static readonly float WalledColliderPadding = 0.04f;
    private static readonly float CeilingColliderSize = 0.01f;
    private static readonly Color CollisionOnColor = Color.green;
    private static readonly Color CollisionOffColor = Color.red;

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

    public bool InputJump { get; set; }
    public float InputMove { get; set; }

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
        UpdateGrounded();
        UpdateCeiling();
        UpdateWalled();
        UpdateJump();
    }

    private void FixedUpdate()
    {
        UpdateMove();
    }

    private void UpdateGrounded()
    {
        var wasGrounded = IsGrounded;

        var bounds = BoxCollider2D.bounds;
        bounds.center = bounds.center.WithY(bounds.center.y - bounds.extents.y);
        bounds.extents = bounds.extents.WithY(GroundedColliderSize / 2);

        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, transform.localEulerAngles.z, Vector2.down, GroundedColliderSize, _layerMask);
        IsGrounded = raycastHit.collider != null;
        DebugDrawBox(bounds, IsGrounded);

        UpdateLand(wasGrounded);
        UpdateFall();
    }

    private void UpdateLand(bool wasGrounded)
    {
        if (wasGrounded != IsGrounded
            && IsGrounded)
        {
            ResetJumpsRemaining();
            IsJumping = false;
            OnLand?.Invoke(this);
        }
    }

    private void UpdateFall()
    {
        if (!IsGrounded
            && !IsFalling
            && Rigidbody2D.velocity.y < 0.0f)
        {
            IsFalling = true;
            OnFall?.Invoke(this);
        }
    }

    private void UpdateCeiling()
    {
        var wasCeiling = IsCeiling;

        var bounds = BoxCollider2D.bounds;
        bounds.center = bounds.center.WithY(bounds.center.y + bounds.extents.y + CeilingColliderSize / 2);
        bounds.extents = bounds.extents.WithY(CeilingColliderSize / 2);

        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, transform.localEulerAngles.z, Vector2.up, CeilingColliderSize, _layerMask);
        IsCeiling = raycastHit.collider != null;
        DebugDrawBox(bounds, IsCeiling);

        if (wasCeiling != IsCeiling
            && IsCeiling)
        {
            OnCeiling?.Invoke(this);
        }
    }

    private void UpdateWalled()
    {
        var wasWalled = IsWalled;

        UpdateWalledLeft();
        UpdateWalledRight();

        IsWalled = IsWalledLeft || IsWalledRight;

        if (wasWalled != IsWalled
            && IsWalled)
        {
            OnWall?.Invoke(this);
        }
    }

    private void UpdateWalledLeft()
    {
        var bounds = BoxCollider2D.bounds;
        bounds.center = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y + GroundedColliderSize / 2);
        bounds.extents = new Vector2(WalledColliderSize / 2, bounds.extents.y - WalledColliderPadding);

        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, transform.localEulerAngles.z, Vector2.left, WalledColliderSize, _layerMask);
        IsWalledLeft = raycastHit.collider != null;
        DebugDrawBox(bounds, IsWalledLeft);
    }

    private void UpdateWalledRight()
    {
        var bounds = BoxCollider2D.bounds;
        bounds.center = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y + GroundedColliderSize / 2);
        bounds.extents = new Vector2(WalledColliderSize / 2, bounds.extents.y - WalledColliderPadding);

        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, transform.localEulerAngles.z, Vector2.right, WalledColliderSize, _layerMask);
        IsWalledRight = raycastHit.collider != null;
        DebugDrawBox(bounds, IsWalledRight);
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

    private void DebugDrawBox(Bounds bounds, bool isOn)
    {
        if (ShowDebug)
        {
            var color = isOn ? CollisionOnColor : CollisionOffColor;
            DebugExtensions.DrawBox(bounds, color);
        }
    }
}

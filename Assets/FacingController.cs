using System;
using UnityEngine;

public class FacingController : MonoBehaviour
{
    public Facing InitialFacing = Facing.Left;

    private Facing _facing = Facing.Invalid;
    public Facing Facing
    {
        get { return _facing; }
        set
        {
            _facing = value;

            var scale = transform.localScale;
            if (_facing == Facing.Left)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = Mathf.Abs(scale.x) * -1;

            transform.localScale = scale;
        }
    }

    public float Direction
    {
        get { return Facing == Facing.Left ? -1 : 1; }
    }

    public void Flip()
    {
        if (_facing == Facing.Left)
            Facing = Facing.Right;
        else if (_facing == Facing.Right)
            Facing = Facing.Left;
    }

    private void Awake()
    {
        Facing = InitialFacing;
    }
}
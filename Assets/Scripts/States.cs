using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayerStates
{
    public abstract void Movement(float move);
    public abstract void OnJump();
}
public class BaseState : IPlayerStates 
{
    public float speed = 3.0f;
    public float jumpForce = 5.0f;
    Rigidbody2D rigidbody;
    Transform transform;
    LayerMask groundMask;
    public BaseState(Rigidbody2D newRigidbody, Transform newTransform, LayerMask newGroundMask) { (rigidbody, transform, groundMask) = (newRigidbody, newTransform, newGroundMask); }
    public override void Movement(float move)
    {
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
    }
    public override void OnJump()
    {
        if (GroundCheck())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }
    bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.65f, groundMask);
        return (hit.collider != null);
    }
}

public class JumpState : IPlayerStates
{
    public override void OnJump()
    {
    }
    public override void Movement(float move) { throw new System.NotImplementedException(); }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem 
{
    public float speed = 1.0f;
    public float jumpForce = 2.0f;
    Rigidbody2D rigidbody;
    Transform transform;
    LayerMask groundMask;

    public MovementSystem(Rigidbody2D newRigidbody, Transform newTransform, LayerMask newGroundMask) { (rigidbody, transform, groundMask) = (newRigidbody, newTransform, newGroundMask); }
    public void Move(float move, bool jump)
    {
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
        if (jump && GroundCheck())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }
    //public void Move(Rigidbody2D rb, Transform thisTransform, float move, bool jump)
    //{
    //    rb.velocity = new Vector2(move * speed, rb.velocity.y);
    //    if (jump)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //    }
    //}
    bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.65f, groundMask);
        return (hit.collider != null);
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class IPlayerStates
{
    public abstract void Movement(float move);
    public abstract void OnJump();
    public abstract void Collision(GameObject gameObject, bool isCollision);
}
public class BaseState : IPlayerStates 
{
    float speed = 3.0f;
    float jumpForce = 5.0f;
    CoyoteTime coyoteTime = new CoyoteTime();
    Rigidbody2D rigidbody;
    Transform transform;
    GameObject item;
    int lastMove = 1;
    public BaseState(GameObject gameObject, int newLastMove) { 
        (rigidbody, transform, lastMove) = 
            (gameObject.GetComponent<Rigidbody2D>(), 
            gameObject.GetComponent<Transform>(),
            newLastMove); }
    public override void Movement(float move)
    {
        if (coyoteTime.isCoyoteTime)
        {
            coyoteTime.coyoteTime += Time.deltaTime;
        }
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
    }
    public override void OnJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, (1 << LayerMask.NameToLayer("Ground")));
        Collider2D collider = hit.collider;
        bool isGrounded = collider != null && (1 << collider.gameObject.layer) != 0;
        //if (isCollision)
        //{
        //    if (collider.CompareTag("Item"))
        //    {
        //        if (isEquipped) { return; }
        //        AllGamesPhysics.instance.ThrowItem(transform.gameObject, collider.gameObject);
        //        rigidbody.gameObject.GetComponent<Player>().ChangeState(collider.GetComponent<ItemData>().itemStatesType, collider.gameObject);
        //        isEquipped = true;
        //    }
        //    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        //    isCollision = false;
        //}
        if (isGrounded || item)
        {
            if (item && (item.GetComponent<ItemData>().state is not ItemStates.Idle)) { return; }
            if (item)
            {
                AllGamesPhysics.instance.PickUpItem(transform.gameObject, item);
                rigidbody.gameObject.GetComponent<Player>().ChangeState(item.GetComponent<ItemData>().itemStatesType, lastMove, item);
            }
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            coyoteTime.isCoyoteTime = false; coyoteTime.isJumped = true;
        }
        if (coyoteTime.isCoyoteTime && coyoteTime.coyoteTime < 1f)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            coyoteTime.isCoyoteTime = false;
        }
    }
    public override void Collision(GameObject gameObject, bool isCollision)
    {
        if (isCollision)
        {
            if (gameObject.CompareTag("Item"))
            {
                item = gameObject;
            }
            if (gameObject.CompareTag("Ground"))
            {
                coyoteTime.isCoyoteTime = false; coyoteTime.coyoteTime = 0f; coyoteTime.isJumped = false;
            }
        }
        else
        {
            if (gameObject.CompareTag("Item"))
            {
                item = null;
            }
            if (gameObject.CompareTag("Ground") && !coyoteTime.isJumped)
            {
                coyoteTime.isCoyoteTime = true; 
            }
        }

    }
}

public class JumpState : IPlayerStates
{
    public float speed = 3.0f;
    public float jumpForce = 5.0f;
    Rigidbody2D rigidbody;
    Transform transform;
    GameObject item;
    int lastMove = 1;
    public JumpState(GameObject player, GameObject thisItem, int newLastMove) {
        (rigidbody, transform, item, lastMove) = 
            (player.GetComponent<Rigidbody2D>(),
            player.GetComponent<Transform>(),
            thisItem, 
            newLastMove); }
    public override void OnJump()
    {
        if (item.GetComponent<ItemData>().state is ItemStates.Equipped)
        {
            AllGamesPhysics.instance.ThrowItem(item, lastMove);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            rigidbody.gameObject.GetComponent<Player>().ChangeState(ItemStatesTypes.Normal, lastMove);
        }
    }
    public override void Movement(float move) {
        if (move != 0) { lastMove = move > 0 ? 1 : lastMove < 0 ? -1 : -1; }
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
    }
    public override void Collision(GameObject gameObject, bool isCollision)
    {
        if (isCollision)
        {
            if (gameObject.CompareTag("ResetBlock"))
            {
                AllGamesPhysics.instance.ThrowItem(item);
                rigidbody.gameObject.GetComponent<Player>().ChangeState(ItemStatesTypes.Normal, lastMove);
            }
        }
    }
}

public enum TypesGround
{
    Ground,
    Item
}
public struct CoyoteTime
{
    public bool isCoyoteTime;
    public bool isJumped;
    public float coyoteTime;
    public float maxCoyoteTime;
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class IPlayerStates
{
    public abstract void Movement(float move);
    public abstract void OnJump();
}
public class BaseState : IPlayerStates 
{
    float speed = 3.0f;
    float jumpForce = 5.0f;
    Rigidbody2D rigidbody;
    Transform transform;
    bool isEquipped = false;
    public BaseState(GameObject gameObject) { 
        (rigidbody, transform) = 
            (gameObject.GetComponent<Rigidbody2D>(), 
            gameObject.GetComponent<Transform>()); }
    public override void Movement(float move)
    {
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
    }
    public override void OnJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, ~(1 << LayerMask.NameToLayer("Player")));
        Collider2D collider = hit.collider;
        bool isGrounded = collider != null && (1 << collider.gameObject.layer) != 0;
        if (isGrounded)
        {
            if (collider.CompareTag("Item") )
            {
                if (isEquipped){return;}
                AllGamesPhysics.instance.ThrowItem(transform.gameObject, collider.gameObject);
                rigidbody.gameObject.GetComponent<Player>().ChangeState(collider.GetComponent<ItemData>().itemStatesType, collider.gameObject);
                isEquipped = true;
            }
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
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
    public JumpState(GameObject player, GameObject thisItem) {
        (rigidbody, transform, item) = 
            (player.GetComponent<Rigidbody2D>(),
            player.GetComponent<Transform>(),
            thisItem); }
    public override void OnJump()
    {
        AllGamesPhysics.instance.PickUpItem(transform.gameObject, item, lastMove);
        rigidbody.gameObject.GetComponent<Player>().ChangeState(ItemStatesTypes.Normal);
    }
    public override void Movement(float move) {
        if (move != 0) { lastMove = move > 0 ? 1 : lastMove < 0 ? -1 : -1; }
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y); 
    }
}

public enum TypesGround
{
    Ground,
    Item
}
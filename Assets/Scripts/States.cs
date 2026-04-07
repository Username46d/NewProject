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
    LayerMask[] groundMask;
    public BaseState(GameObject gameObject) { 
        (rigidbody, transform, groundMask) = 
            (gameObject.GetComponent<Rigidbody2D>(), 
            gameObject.GetComponent<Transform>(), 
            gameObject.GetComponent<Player>().groundMask); }
    public override void Movement(float move)
    {
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
    }
    public override void OnJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, ~(1 << LayerMask.NameToLayer("Player")));
        Collider2D collider = hit.collider;
        bool isGrounded = collider != null && (1 << collider.gameObject.layer) != 0;
        Debug.Log(isGrounded, collider);
        if (isGrounded)
        {
            if (collider.CompareTag("Item"))
            {
                rigidbody.gameObject.GetComponent<Player>().ChangeState(collider.GetComponent<ItemData>().itemStatesType, collider.gameObject);
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
    LayerMask[] groundMask;
    GameObject item;
    int lastMove = 1;
    public JumpState(GameObject player, GameObject thisItem) {
        (rigidbody, transform, groundMask, item) = 
            (player.GetComponent<Rigidbody2D>(),
            player.GetComponent<Transform>(),
            player.GetComponent<Player>().groundMask,
            thisItem); }
    public override void OnJump()
    {
        Vector3 target = transform.position + new Vector3(lastMove * 3f, 1f, 0f);
        item.transform.position = target;
        //AllGamesPhysics.ThrowItem(item, )
        rigidbody.gameObject.GetComponent<Player>().ChangeState(ItemStatesTypes.Normal);
    }
    public override void Movement(float move) {
        if (move != 0) { lastMove = move > 0 ? 1 : -1; }
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y); 
    }
}

public enum TypesGround
{
    Ground,
    Item
}
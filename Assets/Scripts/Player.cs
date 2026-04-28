using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    IPlayerStates playerState;
    private void Awake()
    {
        playerState = new BaseState(gameObject, 1);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));

        Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.red);
    }
    public void Update()
    {
        Movement();
    }
    private void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            playerState.OnJump();
        }
        playerState.Movement(input);
    }
    public void ChangeState(ItemStatesTypes itemStatesType, int lastMove, GameObject item = null){playerState = StatesFabric.NewState(itemStatesType, gameObject, lastMove, item);}
    public void OnCollisionEnter2D(Collision2D collision)
    {
        playerState.Collision(collision.gameObject, true);
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        playerState.Collision(collision.gameObject, false);
    }
    public void OnTriggerEnter2D(Collider2D collision){playerState.Collision(collision.gameObject, true);}
    public void OnTriggerExit2D(Collider2D collision){playerState.Collision(collision.gameObject, false);}
    public void OnChildCollision(Collider2D collision) { playerState.Collision(collision.gameObject, true);  }
}

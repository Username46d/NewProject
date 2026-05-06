using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstance : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            CheckpointManager.Instance.TeleportPlayer();
            if (player.GetStates() is not JumpState)
            {
                AllGamesPhysics.instance.PickUpItem(player.gameObject, CheckpointManager.Instance.GetLastItem());
                player.ChangeState(ItemStatesTypes.JumpObject, 1, CheckpointManager.Instance.GetLastItem());
            }
        }
    }
}
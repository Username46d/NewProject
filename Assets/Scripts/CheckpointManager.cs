using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    GameObject currentCheckpoint;
    GameObject player;
    GameObject lastItem;

    private void Start()
    {
        Instance = this;
    }
    public void UpdateCheckpoint(GameObject checkpoint, GameObject tPlayer)
    {
        currentCheckpoint = checkpoint;
        player = tPlayer;
    }
    public void TeleportPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
    public void SaveLastItem(GameObject thisGameObject)
    {
        lastItem = thisGameObject;
    }
    public GameObject GetLastItem()
    {
        return lastItem;
    }
}

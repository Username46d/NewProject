using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public ItemStates state;
    public float direction = 1f;
    [SerializeField] public ItemStatesTypes itemStatesType;
    public void Start(){state = ItemStates.Idle;}
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Была коллизия");
        Vector2 normal = collision.GetContact(0).normal;
        if (Mathf.Abs(normal.x) > 0.5f)
        {
            AllGamesPhysics.instance.ThrowItem(gameObject, direction * -1f);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.transform.parent != null)
        {
            GetComponentInParent<Player>().OnChildCollision(collision);
        }
        Debug.Log("Была коллизия");
    }
}

//public class MovementBlock : MonoBehaviour
//{ 

//    void 
//}

public enum ItemStatesTypes{
    Normal,
    JumpObject
}
public enum ItemStates
{
    Idle,
    Throw,
    BounceFromWall, 
    ProcessEq,
    Equipped
}

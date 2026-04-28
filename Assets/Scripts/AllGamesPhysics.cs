using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllGamesPhysics : MonoBehaviour
{
    public static AllGamesPhysics instance;
    [SerializeField] float duration = 2f;
    [SerializeField] float throwDuration;
    [SerializeField] Vector3 itemPosition;
    [SerializeField] Vector3 throwingVector;
    [SerializeField] float pauseFalls;
    [SerializeField] float pauseSelection;
    Coroutine thisCoroutine;

    private void Awake()
    {
        instance = this;
    }

    public void PickUpItem(GameObject player, GameObject item)
    {
        item.GetComponent<ItemData>().state = ItemStates.ProcessEq;
        Transform playerTransform = player.transform;
        StartCoroutine(AttractAnimation(playerTransform, item.transform));
    }

    IEnumerator AttractAnimation(Transform target, Transform item)
    {
        Vector3 startPos = item.position;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            Vector3 targetPosition = target.position + itemPosition;
            item.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }
        item.transform.SetParent(target);

        item.GetComponent<Rigidbody2D>().gravityScale = 0f;
        //item.GetComponent<Rigidbody2D>().simulated = false;
        item.GetComponent<Rigidbody2D>().isKinematic = true;
        item.GetComponent<ItemData>().state = ItemStates.Equipped;
    }

    //public void ThrowItem(GameObject player, GameObject item, float direction)
    //{
    //    Debug.Log("ÂÎ ÂÐÅÌß ÁÐÎÑÊÀ" + direction);
    //    item.GetComponent<ItemData>().state = ItemStates.Throw;
    //    item.GetComponent<Rigidbody2D>().simulated = true;
    //    item.transform.SetParent(null);
    //    Debug.Log("Âåêòîð äî " + throwingVector);
    //    Vector3 newThrowingVector = throwingVector;
    //    newThrowingVector.x *= direction;
    //    Debug.Log("Âåêòîð ïîñëå " + throwingVector);
    //    item.GetComponent <Rigidbody2D>().velocity = newThrowingVector;

    //    StartCoroutine(GravityItem(item));
    //}
    public void ThrowItem(GameObject item, float direction)
    {
        if (thisCoroutine != null)
        {
            StopCoroutine(thisCoroutine);
        }
        item.GetComponent<ItemData>().direction = direction;
        thisCoroutine = StartCoroutine(MoveItem(item, direction));
    }
    public void ThrowItem(GameObject item)
    {
        item.GetComponent<ItemData>().state = ItemStates.Throw;
        //item.GetComponent<Rigidbody2D>().simulated = true;
        item.GetComponent<Rigidbody2D>().isKinematic = false;
        item.transform.SetParent(null);
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(GravityItem(item));
    }
    private IEnumerator MoveItem(GameObject item, float direction) // ÏÅÐÅÏÈÑÀÒÜ
    {
        item.GetComponent<ItemData>().state = ItemStates.Throw;
        //item.GetComponent<Rigidbody2D>().simulated = true;
        item.GetComponent<Rigidbody2D>().isKinematic = false;
        item.transform.SetParent(null);
        item.GetComponent<Rigidbody2D>().gravityScale = 0f;
        Vector3 start = item.transform.position;
        Vector3 newThrowingVector = throwingVector; newThrowingVector.x *= direction;
        Vector3 end = start + newThrowingVector;
        float timer = 0f;
        while (item.GetComponent<ItemData>().state is ItemStates.Throw && timer < throwDuration) 
        {
            timer += Time.deltaTime;
            item.transform.position = Vector3.Lerp(start, end, timer/throwDuration);
            yield return null;
        }

        StartCoroutine(GravityItem(item));
    }
    private IEnumerator GravityItem(GameObject item)
    {
        ItemData itemData = item.GetComponent<ItemData>();
        yield return new WaitForSeconds(pauseSelection);
        itemData.state = ItemStates.Idle;
        yield return new WaitForSeconds(pauseFalls);
        if (itemData.state is ItemStates.Idle)
        {
            item.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        bool isGround = false;
        while (!isGround)
        {
            RaycastHit2D hit = Physics2D.Raycast(item.transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                isGround = true;
            }
            yield return null;
        }
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        item.GetComponent<Rigidbody2D>().gravityScale = 0f;
        yield break;
    }
}

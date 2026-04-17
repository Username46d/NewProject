using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllGamesPhysics : MonoBehaviour
{
    public static AllGamesPhysics instance;
    [SerializeField] float duration = 2f;
    [SerializeField] Vector3 itemPosition;
    [SerializeField] Vector3 throwingVector;
    [SerializeField] float gravityItem;
    [SerializeField] float timeOfGravity;

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
        item.GetComponent<Rigidbody2D>().simulated = false;
        item.GetComponent<ItemData>().state = ItemStates.Equipped;
    }

    public void ThrowItem(GameObject player, GameObject item, float direction)
    {
        Debug.Log("ВО ВРЕМЯ БРОСКА" + direction);
        item.GetComponent<ItemData>().state = ItemStates.Throw;
        item.GetComponent<Rigidbody2D>().simulated = true;
        item.transform.SetParent(null);
        Debug.Log("Вектор до " + throwingVector);
        Vector3 newThrowingVector = throwingVector;
        newThrowingVector.x *= direction;
        Debug.Log("Вектор после " + throwingVector);
        item.GetComponent <Rigidbody2D>().velocity = newThrowingVector;

        StartCoroutine(GravityItem(item));
    }
    public void ThrowItem(GameObject player, GameObject item)
    {
        item.GetComponent<ItemData>().state = ItemStates.Throw;
        item.GetComponent<Rigidbody2D>().simulated = true;
        item.transform.SetParent(null);
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(GravityItem(item));
    }
    private IEnumerator GravityItem(GameObject gameObject)
    {
        ItemData itemData = gameObject.GetComponent<ItemData>();
        yield return new WaitForSeconds(0.5f);
        itemData.state = ItemStates.Idle;
        yield return new WaitForSeconds(1.5f);
        if (itemData.state is ItemStates.Idle)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }

    }
}

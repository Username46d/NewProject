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

    public void ThrowItem(GameObject player, GameObject item)
    {
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

        item.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void PickUpItem(GameObject player, GameObject item, float direction)
    {
        item.GetComponent<Rigidbody2D>().simulated = true;
        item.transform.SetParent(null);

        throwingVector.x *= direction;
        item.GetComponent <Rigidbody2D>().velocity = throwingVector;

        item.GetComponent<Rigidbody2D>().gravityScale = gravityItem;
        float timer = 0f;
        while (timer < timeOfGravity)
        {
            Debug.Log(gravityItem);
            Debug.Log(item.GetComponent<Rigidbody2D>().gravityScale);
            timer += Time.deltaTime;
        }
        item.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

}

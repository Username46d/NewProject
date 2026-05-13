using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] Vector2 pushVector;
    Coroutine coroutine;
    [SerializeField] float throwDuration;
    private void OnCollisionEnter2D(Collision2D collision)
    {
     if (collision.gameObject.GetComponent<ItemData>())
        {
            Debug.Log("был контакт");
            Vector3 itemPosition = collision.transform.position;
            Vector2 point = collision.GetContact(0).normal;
            float directionX = point.x < gameObject.transform.position.x ? -1 : 1;
            
            //gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(PushThisObject(directionX));
        }   
    }
    IEnumerator PushThisObject(float directionX)
    {
        Debug.Log("старт короутины");
        Transform item = gameObject.transform;
        Vector3 direction = new Vector2(item.position.x + pushVector.x, item.transform.position.y + pushVector.y); direction.x *= directionX;
        float timer = 0f;
        while (timer < throwDuration)
        {
            timer += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(item.position, direction, timer / throwDuration);
            yield return null;
        }
        yield break;
    }
}

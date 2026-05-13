using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    [SerializeField] float speed;
    int currentPoint;
    bool isMoving = false;

    private void Start()
    {
        currentPoint = 0;
    }
    public void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, points[currentPoint].transform.position) < 0.01f)
        {
            if (currentPoint + 1 == points.Length)
            {
                currentPoint = 0;
            }
            else
            {
                currentPoint += 1;
            }
        }
    }


    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ItemData>())
        {
            Moving();
            collision.gameObject.GetComponent<Transform>().SetParent(transform);
        }
        if (collision.gameObject.GetComponentInChildren<ItemData>())
        {
            Moving();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemData>())
        {
            Moving();
        }
    }
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<ItemData>())
    //    {
    //        isMoving = false;
    //    }
    //}















    //public void TryMoving(bool isMoving, GameObject collision)
    //{
    //    collision.gameObject.transform.SetParent(gameObject.transform);
    //    isMoving = true;
    //}
    //public void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<ItemData>())
    //    {
    //        isMoving = false;
    //    }
    //}
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<ItemData>())
    //    {
    //        isMoving = false;
    //    }
    //}
    //public void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<ItemData>())
    //    {
    //        collision.gameObject.transform.SetParent(gameObject.transform);
    //        isMoving = true;
    //    }
    //    else if (collision.gameObject.GetComponentInChildren<ItemData>())
    //    {
    //        isMoving = true;
    //    }
    //}
    //public void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<ItemData>())
    //    {
    //        collision.gameObject.transform.SetParent(gameObject.transform);
    //        isMoving = true;
    //    }
    //    else if (collision.gameObject.GetComponentInChildren<ItemData>())
    //    {
    //        isMoving = true;
    //    }
    //}
}

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
    private void Update()
    {
        if (isMoving)
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
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ItemData>() || collision.gameObject.GetComponentInChildren<ItemData>())
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
            isMoving = true;
        } 
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ItemData>() || collision.gameObject.GetComponentInChildren<ItemData>())
        {
            isMoving = false;
            collision.gameObject.transform.SetParent(null);
        }
    }
}

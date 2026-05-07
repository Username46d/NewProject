using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    [SerializeField] CameraStatesType cameraType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraMovement.Instance.ChangeStates(cameraType, CameraMovement.Instance.gameObject, collision.gameObject.GetComponent<Transform>());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    IPlayerStates playerStates;
    [SerializeField] LayerMask groundMask;
    private void Awake()
    {
        playerStates = new BaseState(gameObject.GetComponent<Rigidbody2D>(), gameObject.GetComponent<Transform>(), groundMask);
    }
    public void Update()
    {
        Movement();
    }
    private void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        bool jump = false;
        if (Input.GetButtonDown("Jump"))
        {
            playerStates.OnJump();
        }
        playerStates.Movement(input);
    }
}

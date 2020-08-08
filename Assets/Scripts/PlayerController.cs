﻿using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    public Rigidbody rb;
    //public Animator anims;
    public Transform mcamera;
    public int points = 0;
    [Header("Movement")]
    public float moveSpeed = 5f;
    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    public int fruit;

    [Header("State")]
    public float speed = 0;

    [Header("Animator")]
    private Animator anims;
    public bool isHolding = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = moveSpeed;   
        anims = gameObject.GetComponentInChildren<Animator>();
    }
    
    int getPoints()
    {
        return points;
    }

    // Update is called once per frame
    void Update()
    {
        InputUpdate();
        Movement();
        MovementAnimation();
        if(isHolding) {
            HoldingObject();
        }
        else if(isHolding == false)
        {
            anims.SetBool("HasObject", false);
        }
        PlayerRotation();
    }

    void Movement() {
        Vector3 velocity = new Vector3(horizontalInput, 0, verticalInput) * speed;
        rb.velocity = velocity;
    }

    void InputUpdate() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // anims.SetFloat("horizontal_float", horizontalInput);
        // anims.SetFloat("vertical_float", verticalInput);
    }
    void HoldingObject() {
        anims.SetBool("HasObject",true);
    }
    // bool getHold() {
    //     return this.isHolding;
    // }
    // void setHold(bool value) {
    //     this.isHolding = true;
    // }
    void PlayerRotation() {
        // H: 0 V: 1 --> forward
        if (horizontalInput == 0 && verticalInput > 0 ) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            mcamera.rotation = Quaternion.Euler(90, 0, 0);
        } 
        // H: 0 V: -1 --> backwards
        if (horizontalInput == 0 && verticalInput < 0 ) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            mcamera.rotation = Quaternion.Euler(90, -180, 180);
        }
        // H: -1 V: 0 --> left
        if (horizontalInput > 0 && verticalInput == 0 ) {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            mcamera.rotation = Quaternion.Euler(90, -90, 270);
        } 
        // H: 1 V: 0 --> right
        if (horizontalInput < 0 && verticalInput == 0 ) {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            mcamera.rotation = Quaternion.Euler(90, -270, 90);
        }
    }
    void MovementAnimation() {
        float FloatSpeed = Mathf.Abs((horizontalInput+verticalInput)*speed);
        anims.SetFloat("speed", FloatSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cake")
        {
            if (isHolding)
            {
                //points = points + 1;
                isHolding = false;
            }
            Debug.Log("touched cake");
            Debug.Log(points);
        }
    }

}

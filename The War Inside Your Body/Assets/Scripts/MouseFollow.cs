﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{

    private Vector3 mousePosition;
    public float moveSpeed = 10f;
    public Camera camera2;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = camera2.ScreenToWorldPoint(mousePosition);
        
        //mousePosition.x += -5.851833f;
        //mousePosition.y -= 3.290733f;
        //mousePosition.z = 0;

        mousePosition.y = 0;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Mouse0))
            transform.Rotate(0, 3f, 0);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Mouse1))
            transform.Rotate(0, -3f, 0);


        if (Input.GetKey(KeyCode.Mouse2))
        {
            mousePosition.x = 10000000;
        }
        transform.position = mousePosition;
        //transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);

    }
}
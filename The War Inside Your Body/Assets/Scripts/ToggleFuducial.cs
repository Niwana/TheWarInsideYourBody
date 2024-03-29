﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFuducial : MonoBehaviour
{
    public bool enablePlane = false;

    [HideInInspector]
    public bool isAttached = false;

    // Start is called before the first frame update
    void Start()
    {
        enablePlane = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            enablePlane = !enablePlane;


        if (enablePlane)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}

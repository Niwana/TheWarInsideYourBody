﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableProtein : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(0.5f, 1, 0.2f);
    }
}
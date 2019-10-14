using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //activate the screen to be used, with resolution
        Display.displays[1].Activate(1280, 800, 60);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

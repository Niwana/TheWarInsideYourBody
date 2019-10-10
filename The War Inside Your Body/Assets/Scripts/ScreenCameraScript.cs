using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //activate the screen to be used, with resolution
        Display.displays[2].Activate(1920, 1080, 60);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

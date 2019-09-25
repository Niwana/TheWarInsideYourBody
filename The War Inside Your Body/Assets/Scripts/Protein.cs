using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protein : MonoBehaviour
{
    //float angleTreshold = 10;
    //float rotationMin, rotationMax;

    public static bool isOverlapping;
    public Collider collidingProtein;


    // Update is called once per frame
    void Update()
    {
        //rotationMin = transform.rotation.eulerAngles.z - angleTreshold;
        //rotationMax = transform.rotation.eulerAngles.z + angleTreshold;

        //Debug.Log("Min: " + rotationMin + " , Max: " + rotationMax);
        //Debug.Log(isOverlapping);
    }

    private void OnTriggerEnter(Collider other)
    {
        isOverlapping = true;
        collidingProtein = other;
    }
    private void OnTriggerExit(Collider other)
    {
        isOverlapping = false;
        collidingProtein = null;
    }

    public bool getIsOverlapping()
    {
        return isOverlapping;
    }

    public Collider GetCollider()
    {
        return collidingProtein;
    }
}

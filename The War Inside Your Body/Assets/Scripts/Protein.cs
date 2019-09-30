using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protein : MonoBehaviour
{
    //float angleTreshold = 10;
    //float rotationMin, rotationMax;

    private bool isOverlapping;
    private Collider collidingProtein;

    private Color startColor;

        private void Start()
    {
        startColor = gameObject.GetComponentInChildren<MeshRenderer>().materials[0].GetColor("_Color");
    }


    // Update is called once per frame
    void Update()
    {
        //rotationMin = transform.rotation.eulerAngles.z - angleTreshold;
        //rotationMax = transform.rotation.eulerAngles.z + angleTreshold;

        if (getIsOverlapping())
            gameObject.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_Color", Color.white);
        else
            gameObject.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_Color", startColor);
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

    public bool setIsOverlapping()
    {
        return isOverlapping;
    }

    public Collider GetCollider()
    {
        return collidingProtein;
    }
}

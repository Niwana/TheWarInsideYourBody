using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protein : MonoBehaviour
{
    private bool isOverlapping;
    private Collider collidingFuducial;

    private GameObject ring;
    public float ringSizeSpeed = 1f;
    public float targetRingSize = 20;

    List<GameObject> markers = new List<GameObject>();

    private void Start()
    {
        ring = this.gameObject.transform.GetChild(4).gameObject;
    }


    // Update is called once per frame
    void Update()
    {

        // If the fuducial object collides with the protein
        if (collidingFuducial != null)
        {
            if (collidingFuducial.gameObject.name == "FuducialObject(Clone)")
            {
                this.gameObject.transform.position = collidingFuducial.transform.position;
                this.gameObject.transform.rotation = collidingFuducial.transform.rotation;
            }
        }

        // Run animation on the ring
        if (isOverlapping)
        {
            if (ring.transform.localScale.x <= targetRingSize)
            {
                ring.transform.localScale += new Vector3(ringSizeSpeed, ringSizeSpeed, ringSizeSpeed);
                
            }
        }
        else
        {
            if (ring.transform.localScale.x >= 1)
            {
                ring.transform.localScale -= new Vector3(ringSizeSpeed, ringSizeSpeed, ringSizeSpeed);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isOverlapping = true;

        if (other.gameObject.name == "FuducialObject(Clone)")
        {
            collidingFuducial = other;

            
            markers.Add(this.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject);
            markers.Add(this.gameObject.transform.GetChild(2).transform.GetChild(0).gameObject);
            markers.Add(this.gameObject.transform.GetChild(3).transform.GetChild(0).gameObject);

            ActivateMarkers();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isOverlapping = false;
        collidingFuducial = null;

        DeactivateMarkers();
    }

    private void ActivateMarkers()
    {
        foreach (var marker in markers)
        {
            marker.SetActive(true);
        }
    }    
    
    private void DeactivateMarkers()
    {
        foreach (var marker in markers)
        {
            marker.SetActive(false);
        }
    }

    public bool getIsOverlapping()
    {
        return isOverlapping;
    }

    public bool setIsOverlapping(bool _bool)
    {
        isOverlapping = _bool;
        return isOverlapping;
    }
}

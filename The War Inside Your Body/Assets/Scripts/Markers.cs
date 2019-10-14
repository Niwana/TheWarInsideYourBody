using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markers : MonoBehaviour
{
    public String targetMarker;

    public GameObject protein_1_V;

    public GameObject parent;

    public delegate void MatchAction();
    public static event MatchAction OnMarkerMatch;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == targetMarker)
        {
            //TODO: use these events instead of the manual calls below it
            //use the event to announce the trigger, add in listener functions i.e. animation play and disable etc
            //e.g. marker.OnMarkerMatch += DisableFuducialObject(); in the parent object
            if (OnMarkerMatch != null)
                OnMarkerMatch();

            protein_1_V.GetComponent<Animator>().SetTrigger("playFlyIn");

            //Disable the fuducial object
            this.gameObject.GetComponentInParent<FuducialObjects>().DisableFuducialObject();

            //Spawn proteins on the tabletop
            parent.GetComponent<FuducialObjects>().SpawnProteins();

            //Play docking animation on the tabletop
 
        }
    }
}

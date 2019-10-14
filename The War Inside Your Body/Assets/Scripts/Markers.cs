using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markers : MonoBehaviour
{
    public String targetMarker;

    public GameObject protein_1;

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
        if (other.name == targetMarker)
        {
            protein_1_V.GetComponent<Animator>().SetTrigger("playFlyIn");

            //Disable the fuducial object
            this.gameObject.GetComponentInParent<FuducialObjects>().DisableFuducialObject();

            //Spawn new proteins on the tabletop
            this.gameObject.GetComponentInParent<FuducialObjects>().SpawnProteins();

            //Play docking animation on the tabletop


        }
    }
}

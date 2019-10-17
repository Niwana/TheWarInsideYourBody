﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markers : MonoBehaviour
{
    public String targetMarker;

    public GameObject proteinToAnimate;

    public GameObject parent;

    public delegate void MatchAction();
    public static event MatchAction OnMarkerMatch;

    private static int nextMarkerMatch = 0;
    private static string[] markerMatches = { "MarkerE", "MarkerA", "MarkerF" };

    // Start is called before the first frame update
    void Start()
    {
        //line = Instantiate(Resources.Load("Line")) as LineRenderer;
        //line.positionCount = 2;
        //line.material = new Material(Shader.Find("Sprites/Default"));
        //line.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == targetMarker && markerMatches[nextMarkerMatch] == targetMarker)
        {
            //TODO: use these events instead of the manual calls below it
            //use the event to announce the trigger, add in listener functions i.e. animation play and disable etc
            //e.g. marker.OnMarkerMatch += DisableFuducialObject(); in the parent object
            if (OnMarkerMatch != null)
                OnMarkerMatch();

            nextMarkerMatch++;

            proteinToAnimate.GetComponent<Animator>().SetTrigger("playFlyIn");

            //Disable the fuducial object
            this.gameObject.GetComponentInParent<FuducialObjects>().DisableFuducialObject();

            //Spawn proteins on the tabletop
            parent.GetComponent<FuducialObjects>().SpawnProteins();

            //Make marker invisible to show connection instead
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            //this.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            this.parent.GetComponent<FuducialObjects>().SpawnConnectionLine(other.gameObject.GetComponent<Markers>().parent.transform.position);

            //Play docking animation on the tabletop

        }
    }
}

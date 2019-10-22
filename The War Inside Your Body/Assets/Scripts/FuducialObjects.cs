﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuducialObjects : MonoBehaviour
{
    private bool isOverlapping;
    private Collider collidingFuducial;
    private GameObject ring;
    public bool isDisabled = false;

    public float ringSizeSpeed = 1f;
    private float startScale;
    public float targetRingMaxSize = 200;
    public float targetRingMinSize = 100;

    public GameObject proteinToSpawn;
    public GameObject proteinToSpawn2;

    public int spawnAmountProtein_1;
    public int spawnAmountProtein_2;

    List<GameObject> markers = new List<GameObject>();

    public Vector3 matchTargetPosition = new Vector3(); //-4.79f, 0f, -15.5f
    public Vector3 matchTargetRotation = Vector3.back;

    private Vector3 velocity = Vector3.zero; //var used for movement damping, just leave this

    public LineRenderer line;

    public GameObject proteinToAnimate;

    private GameObject collidingProtein;
    public Color connectionColor = new Color(1f, 0.7843137f, 0f);
    public float connectionColorIntensity = 6;


    private void Start()
    {
        ring = this.gameObject.transform.GetChild(5).gameObject;

        proteinToAnimate = GameObject.Find(proteinToAnimate.name);

        startScale = ring.transform.localScale.x;

        //Save all markers in a list and disable them at start
        foreach (Transform child in transform)
        {
            if (child.tag == "Marker" && child.transform.childCount > 0)
            {
                markers.Add(child.transform.GetChild(0).gameObject);
                child.GetChild(0).gameObject.SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        // If the fuducial object collides with the protein
        if (collidingFuducial != null)
        {
            if ((collidingFuducial.gameObject.name == "FuducialObject(Clone)" || collidingFuducial.gameObject.name == "FuducialObject") && !isDisabled)
            {
                if (collidingFuducial.transform.position.x <= transform.position.x + 1000) //put range for safety
                {
                    MoveTo(collidingFuducial.transform.position, collidingFuducial.transform.rotation, 0.02f, 360);
                    //this.gameObject.transform.position = collidingFuducial.transform.position;
                    //this.gameObject.transform.rotation = collidingFuducial.transform.rotation;
                }
            }
        }

        // Run animation on the ring
        if (!isDisabled)
        {
            if (isOverlapping)
            {
                if (ring.transform.localScale.x <= targetRingMaxSize)
                {
                    ring.transform.localScale += new Vector3(ringSizeSpeed, ringSizeSpeed, ringSizeSpeed);
                }
            }
            else
            {
                if (ring.transform.localScale.x > startScale)
                {
                    ring.transform.localScale -= new Vector3(ringSizeSpeed, ringSizeSpeed, ringSizeSpeed);

                }
            }
        }
        else 
        {
            //note/todo: this still triggers the marker collision twice if not perfectly rotated?
            //TODO: not hardcode the target position. i guess you can define it in scene
            MoveTo(matchTargetPosition, Quaternion.LookRotation(matchTargetRotation), 0.3f, 180f);
            

            //Decrease ring size
            if (ring.transform.localScale.x > targetRingMinSize)
            {
                ring.transform.localScale -= new Vector3(ringSizeSpeed, ringSizeSpeed, ringSizeSpeed);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isOverlapping = true;

        if ((other.gameObject.name == "FuducialObject(Clone)" || other.gameObject.name == "FuducialObject") && !isDisabled)
        {
            collidingFuducial = other;

            ActivateMarkers();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isOverlapping = false;
        collidingFuducial = null;

        if (!isDisabled)
        {
            DeactivateMarkers();
        }
    }

    private void ActivateMarkers()
    {
        foreach (var marker in markers)
        {
            if (!marker.GetComponent<Markers>().shownOnConnection)
            {
                marker.SetActive(true);
            }
        }
    }    
    
    private void DeactivateMarkers()
    {
        foreach (var marker in markers)
        {
            marker.SetActive(false);
        }
    }


    public void OnConnection(GameObject otherProtein)
    {
        collidingProtein = otherProtein;

        //Disable the fuducial object
        DisableFuducialObject();

        //Spawn proteins on the tabletop
        SpawnProteins();

        PlayAnimation();

        //Spawn connection line
        if (otherProtein.GetComponent<FuducialObjects>() != null)
            SpawnConnectionLine(otherProtein.GetComponent<FuducialObjects>().matchTargetPosition);
        else
            SpawnConnectionLine(otherProtein.transform.position); //If it is a root protein
        

        foreach (var marker in markers)
        {
            if (marker.GetComponent<Markers>().shownOnConnection)
            {
                marker.SetActive(true);
            }
        }

    }

    public void DisableFuducialObject()
    {
        isDisabled = true;

        //Set color of ring after a connection is made
        ring.gameObject.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", connectionColor * connectionColorIntensity);

        if (collidingProtein.GetComponent<FuducialObjects>() == null)
            collidingProtein.gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<Renderer>().material.SetColor
                ("_EmissionColor", connectionColor * connectionColorIntensity);
    }


    public void SpawnProteins()
    {
        for (int i = 0; i < spawnAmountProtein_1; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 10), 0f, Random.Range(-2, 0));
            if (proteinToSpawn != null)
            {
                Instantiate(proteinToSpawn, pos, Quaternion.identity);
            }
        }
        for (int i = 0; i < spawnAmountProtein_2; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 10), 0f, Random.Range(-2, 0));
            if (proteinToSpawn2 != null)
            {
                Instantiate(proteinToSpawn2, pos, Quaternion.identity);
            }
        }
    }

    public void PlayAnimation()
    {
        proteinToAnimate.GetComponent<Animator>().SetTrigger("playFlyIn");
    }

    public void SpawnConnectionLine(Vector3 otherPosition)
    {
        Vector3 thisPosition = matchTargetPosition; //this.gameObject.transform.position;
        
        Vector3 middlePoint = Vector3.Normalize(otherPosition - thisPosition) + thisPosition;
        line = Instantiate(line);
        line.numCornerVertices = 5;
        line.transform.position = middlePoint;
        line.transform.rotation = Quaternion.LookRotation(Vector3.Normalize(otherPosition - thisPosition));
        line.SetPosition(0, new Vector3(0.0f, 0.0f, 0.0f));
        line.SetPosition(1, new Vector3(0.0f, 0.0f, Vector3.Distance(thisPosition, otherPosition)));
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


    ///<summary>
    ///smoothly move self to target position at the current frame. 
    ///it's not super intuitive: reachTime is time to reach position (lower=faster), degRate is in degrees/second.
    ///</summary>
    public void MoveTo(Vector3 position, Quaternion rotation, float reachTime, float degRate)
    {
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, reachTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, degRate * Time.deltaTime);
    }

}

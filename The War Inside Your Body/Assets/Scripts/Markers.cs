using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markers : MonoBehaviour
{
    public String targetMarker;
    public bool shownOnConnection;

    //public GameObject proteinToAnimate;

    public GameObject parent;

    public delegate void MatchAction();
    public static event MatchAction OnMarkerMatch;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == targetMarker)
        {
            //Make marker invisible to show connection instead
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);

            parent.GetComponent<FuducialObjects>().OnConnection(other.gameObject.GetComponent<Markers>().parent);
        }

        /*
        if (other.name == targetMarker)
        {
            //Make marker invisible to show connection instead
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);

            parent.GetComponent<FuducialObjects>().OnConnection(other.gameObject.GetComponent<Markers>().parent);
        }
        */
    }
}

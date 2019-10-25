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

    public static int p9CollisionCounter;
    public static bool p7Connected;
    public static bool p8Connected;
    public static bool p9Connected;


    private void Start()
    {
        p9CollisionCounter = 0;
        p7Connected = false;
        p8Connected = false;
        p9Connected = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == targetMarker)
        {
            if (parent.name == "protein_9_I(Clone)")
            {
                p9CollisionCounter++;
                if (p9CollisionCounter < 3)
                    return;
            }

            if (parent.name == "protein_7_I(Clone)")
                p7Connected = true;
            if (parent.name == "protein_8_I(Clone)")
                p8Connected = true;
            if (parent.name == "protein_9_I(Clone)")
                p9Connected = true;

            //Make marker invisible to show connection instead
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);

            parent.GetComponent<FuducialObjects>().OnConnection(other.gameObject.GetComponent<Markers>().parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == targetMarker && parent.name == "protein_9_I(Clone)")
        {
            p9CollisionCounter--;
        }
    }
}

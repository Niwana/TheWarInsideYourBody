using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuducialObjects : MonoBehaviour
{
    private bool isOverlapping;
    private Collider collidingFuducial;
    private GameObject ring;

    private bool isDisabled = false;

    public float ringSizeSpeed = 0.1f;
    public float targetRingSize = 2;

    private List<GameObject> markers = new List<GameObject>();

    public GameObject fuducialToSpawn;

    public int numberOfProtein_1;

    private void Start()
    {
        ring = this.gameObject.transform.GetChild(4).gameObject;

        // Spawn a protein inside of the fuducial object
        GameObject protein = Instantiate(proteinToSpawn, this.gameObject.transform.GetChild(0));
        protein.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {

        // If the fuducial object collides with the protein
        if (collidingFuducial != null)
        {
            if (collidingFuducial.gameObject.name == "FuducialObject(Clone)"  && !isDisabled)
            {
                this.gameObject.transform.position = collidingFuducial.transform.position;
                this.gameObject.transform.rotation = collidingFuducial.transform.rotation;
            }
        }

        // Run animation on the ring
        if (!isDisabled)
        {
            if (isOverlapping)
            {
                if (ring.transform.localScale.x <= targetRingSize)
                {
                    ring.transform.localScale += new Vector3(ringSizeSpeed, ringSizeSpeed, ringSizeSpeed);

                }
            }
            else
            {
                if (ring.transform.localScale.x > 1)
                {
                    ring.transform.localScale -= new Vector3(ringSizeSpeed, ringSizeSpeed, ringSizeSpeed);

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isOverlapping = true;

        //Debug.Log("Collision");

        if (other.gameObject.name == "FuducialObject(Clone)" && !isDisabled)
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

        if (!isDisabled)
        {
            DeactivateMarkers();
        }
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

    public void DisableFuducialObject()
    {
        isDisabled = true;
    }

    public void SpawnProteins()
    {
        Debug.Log(this.gameObject.name);


        

        if (this.gameObject.name == "Protein_1_I")
        {
            Debug.Log("Spawn 1");

            for (int i = 0; i < numberOfProtein_1; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-30, 20), 0f, Random.Range(-5, 0));
                GameObject newProtein = Instantiate(fuducialToSpawn, pos, Quaternion.identity);
            }
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

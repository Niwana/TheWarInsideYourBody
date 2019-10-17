using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuducialObjects : MonoBehaviour
{
    private bool isOverlapping;
    private Collider collidingFuducial;
    private GameObject ring;
    public bool isDisabled = false;

    public float ringSizeSpeed = 1f;
    public float targetRingSize = 20;

    public GameObject proteinToSpawn;
    public GameObject proteinToSpawn2;

    public int spawnAmountProtein_1;
    public int spawnAmountProtein_2;

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
            if ((collidingFuducial.gameObject.name == "FuducialObject(Clone)" || collidingFuducial.gameObject.name == "FuducialObject") && !isDisabled)
            {
                if (collidingFuducial.transform.position.x <= transform.position.x + 1000) //put range for safety
                {
                    this.gameObject.transform.position = collidingFuducial.transform.position;
                    this.gameObject.transform.rotation = collidingFuducial.transform.rotation;
                }
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

        if ((other.gameObject.name == "FuducialObject(Clone)" || other.gameObject.name == "FuducialObject") && !isDisabled)
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
        //Disable ring when connection is made
        ring.gameObject.SetActive(false);
    }

    public void SpawnProteins()
    {
        Debug.Log(this.name);

        //First docking
        if (this.name == "Protein_1_I")
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

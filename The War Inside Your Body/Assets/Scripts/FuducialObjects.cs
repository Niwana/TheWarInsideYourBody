using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FuducialObjects : MonoBehaviour
{
    private bool isOverlapping;
    private Collider collidingFuducial;
    private GameObject ring;
    public bool isDisabled = false;
    private bool hasFuducial = false;

    private float ringSizeSpeed = 5f;
    private float startScale;
    public float targetRingMaxSize = 300;
    public float targetRingMinSize = 150;

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

    public static float MinSpawnDistance = 3f;
    public Vector3 SpawnPosition;
    private bool SpawnPositionReached = true;


    private void Start()
    {
        ring = this.gameObject.transform.GetChild(5).gameObject;

        proteinToAnimate = GameObject.Find(proteinToAnimate.name);

        startScale = ring.transform.localScale.x*1.5f;
        ring.transform.localScale = new Vector3(startScale, startScale, startScale);
        targetRingMaxSize = 300;

        //Save all markers in a list and disable them at start
        foreach (Transform child in transform)
        {
            if (child.tag == "Marker" && child.transform.childCount > 0)
            {
                markers.Add(child.transform.GetChild(0).gameObject);
                child.GetChild(0).gameObject.SetActive(false);
            }
        }
        OnConnMatch.AddListener(GameObject.Find("Audio Manager").GetComponent<AudioEventScript>().ProteinConnectHandler);
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
                    MoveTo(collidingFuducial.transform.position, collidingFuducial.transform.rotation, 0.03f, 180);
                    SpawnPositionReached = true;
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

        if(Vector3.Distance(transform.position, SpawnPosition) < 0.2f)
        {
            SpawnPositionReached = true;
        }
        if(!SpawnPositionReached)
        {
            MoveTo(SpawnPosition, Quaternion.LookRotation(Vector3.back), 2f, 20f);
        }

        //Add animation to marker on activate
        foreach (var marker in markers)
        {
            if (marker.activeSelf)
                IncreaseSize(marker);
        }
    }

    [System.Serializable]
    public class GrabEvent : UnityEvent<GameObject> { }
    public GrabEvent OnGrab = new GrabEvent();

    private void OnTriggerEnter(Collider other)
    {
        isOverlapping = true;

        if ((other.gameObject.name == "FuducialObject(Clone)" || other.gameObject.name == "FuducialObject") && !isDisabled)
        {
            collidingFuducial = other;

            ActivateMarkers();
            OnGrab.Invoke(this.gameObject);
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
            marker.transform.localScale = new Vector3(0, 0, 0); //Set the scale to 0 so that the increase 
                                                                //size animation plays each time

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


    [System.Serializable]
    public class ConnectionEvent : UnityEvent<GameObject, GameObject> { }
    public ConnectionEvent OnConnMatch;// = new ConnectionEvent();

    public void OnConnection(GameObject otherProtein)
    {
        collidingProtein = otherProtein;

        //Disable the fuducial object
        DisableFuducialObject();

        //Spawn proteins on the tabletop
        SpawnProteins();

        PlayAnimation();

        //object.OnConnMatch += handler; public void handler(GameObject protein1, GameObject protein2){}
        Debug.Log("invoking callback");
        OnConnMatch.Invoke(this.gameObject, otherProtein);


        //Spawn connection line
        if (otherProtein.GetComponent<FuducialObjects>() != null && gameObject.name != "protein_9_I(Clone)")
            SpawnConnectionLine(otherProtein.GetComponent<FuducialObjects>().matchTargetPosition);
        else if (gameObject.name != "protein_9_I(Clone)")
            SpawnConnectionLine(otherProtein.transform.position); //If it is a root protein

        //Spawn the second root protein after P3 has connected
        if (otherProtein.name == "protein_3_I(Clone)")
        {
            GameObject.FindGameObjectWithTag("Root").transform.position = new Vector3(4.83f, 0f, -23.16f);
        }


        //Protein 9 special
        if (gameObject.name == "protein_9_I(Clone)")
        {
            foreach (var marker in markers)
            {
                marker.SetActive(false);
            }
            SpawnConnectionLine(new Vector3(5, 0, -15));
            SpawnConnectionLine(new Vector3(-3, 0, -7));
            SpawnConnectionLine(new Vector3(13, 0, -7));

            StartCoroutine(PlayBacteriaBurst());
        }

        foreach (var marker in markers)
        {
            if (marker.GetComponent<Markers>().shownOnConnection)
            {
                marker.SetActive(true);
            }
        }

        //Do stuff when protien 7 & 8 have connected
        if (Markers.p7Connected && Markers.p8Connected)
        {
            //Play punching animation
            GameObject protein6 = GameObject.Find("protein_6_V");
            protein6.GetComponent<Animator>().SetTrigger("playPunch");

            GameObject membrane = GameObject.Find("Membrane");
            membrane.GetComponent<Animator>().SetTrigger("playDestruction");
        }

        if (Markers.p9Connected)
        {
            GameObject membrane = GameObject.Find("Membrane");
            membrane.GetComponent<Animator>().SetTrigger("playDestructionSecond");
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
        Vector3 offScreenSpawnPosition = new Vector3(-1f, 0f, 10f);
        for (int i = 0; i < spawnAmountProtein_1; i++)
        {
            Vector3 pos = FindSpawnPosition();
            if (proteinToSpawn != null)
            {
                GameObject spawnedProtein = Instantiate(proteinToSpawn, offScreenSpawnPosition, Quaternion.identity);
                spawnedProtein.GetComponent<FuducialObjects>().SpawnPosition = pos;
                spawnedProtein.GetComponent<FuducialObjects>().SpawnPositionReached = false;
            }
        }
        for (int i = 0; i < spawnAmountProtein_2; i++)
        {
            Vector3 pos = FindSpawnPosition();
            if (proteinToSpawn2 != null)
            {
                GameObject spawnedProtein = Instantiate(proteinToSpawn2, offScreenSpawnPosition, Quaternion.identity);
                spawnedProtein.GetComponent<FuducialObjects>().SpawnPosition = pos;
                spawnedProtein.GetComponent<FuducialObjects>().SpawnPositionReached = false;
            }
        }
    }

    private Vector3 FindSpawnPosition()
    {
        GameObject[] tableProteins = GameObject.FindGameObjectsWithTag("TableProtein");
        Vector3[] tableProteinPositions = new Vector3[tableProteins.Length];

        for (int i = 0; i < tableProteins.Length; i++)
        {
            tableProteinPositions[i] = tableProteins[i].transform.position;
        }

        Vector3 pos = new Vector3(0f, 0f, 0f);

        bool suitablePosition = false;
        while (!suitablePosition)
        {
            pos = new Vector3(Random.Range(-18, 14), 0f, Random.Range(-2, 0));
            suitablePosition = true;
            foreach (Vector3 existingPosition in tableProteinPositions)
            {
                if(Vector3.Distance(pos, existingPosition) < MinSpawnDistance)
                {
                    suitablePosition = false;
                }
            }
        }

        return pos;
    }

    IEnumerator PlayBacteriaBurst()
    {
        yield return new WaitForSeconds(3f);

        GameObject bacteriaBurst = GameObject.Find("Bacteria burst");
        bacteriaBurst.GetComponent<ParticleSystem>().Play();
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


    private void IncreaseSize(GameObject _object)
    {
        if (_object.transform.localScale.x <= 1)
        {
            _object.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
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

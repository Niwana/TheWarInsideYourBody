using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventScript : MonoBehaviour
{

    public AudioSource MembraneNarrative;
    public AudioSource P0Narrative;
    public AudioSource P1Narrative;
    public AudioSource P3Narrative;
    public AudioSource P8Narrative;
    public AudioSource P9Narrative;

    // Start is called before the first frame update
    void Start()
    {
        MembraneNarrative.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProteinConnectHandler(GameObject protein0, GameObject protein1)
    {
        Debug.Log("protein0: " + protein0.name + ". proetin1: " + protein1.name);
        //play two tone chord
        //can't be bothered with 3+ tone chord rn because ugh
        //also it doesn't really sound good past 3 notes
        protein0.GetComponent<ProteinAudioScript>().PlayPing();
        protein1.GetComponent<ProteinAudioScript>().PlayPing();


        if (protein0.name.StartsWith("protein_1") && protein1.name.StartsWith("protein_2"))
        {
            //do stuff: play narration
            
        }
    }

    public void ProteinGrabHandler(GameObject grabbed)
    {
        grabbed.GetComponent<ProteinAudioScript>().PlayPing();

    }



}

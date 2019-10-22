using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventScript : MonoBehaviour
{

    public AudioSource narration1;
    public AudioSource narration2;
    public AudioSource narration3;
    public AudioSource narration4;
    public AudioSource narration5;
    public AudioSource narration6;
    public AudioSource narration7;

    // Start is called before the first frame update
    void Start()
    {
        
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

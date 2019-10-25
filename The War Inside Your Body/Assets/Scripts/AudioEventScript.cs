using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventScript : MonoBehaviour
{

    public AudioSource BackgroundMusic;
    public AudioSource MembraneNarrative;
    public AudioSource P0Narrative;
    public AudioSource P1Narrative;
    public AudioSource P3Narrative;
    public AudioSource P8Narrative;
    public AudioSource P9Narrative;

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

        Debug.Log("protein connect! " + protein0.name + " and " + protein1.name);
        VideoScript videoscr = GameObject.Find("GameManager").GetComponent<VideoScript>();
        if (videoscr.IntroVideo.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying)
        {
            videoscr.IntroVideoOver(videoscr.IntroVideo.GetComponent<UnityEngine.Video.VideoPlayer>());
        }
        if (MembraneNarrative.isPlaying)
            MembraneNarrative.Stop();


        if (protein0.name.StartsWith("protein_1") && protein1.name.StartsWith("StartProtein"))
        {
            if (P0Narrative.isPlaying)
                P0Narrative.Stop();
            P1Narrative.Play();
        } else if (protein0.name.StartsWith("protein_3") && protein1.name.StartsWith("protein_2"))
        {
            if (P1Narrative.isPlaying)
                P1Narrative.Stop();
            P3Narrative.Play();
        } else if (Markers.p7Connected && Markers.p8Connected)
        {
            if (P3Narrative.isPlaying)
                P3Narrative.Stop();
            P8Narrative.Play();// -50 11.9155 84.7 44.44
        } else if (protein0.name.StartsWith("protein_9"))
        {
            if (P8Narrative.isPlaying)
                P8Narrative.Stop();
            P9Narrative.Play();
            StartCoroutine(PlayOutro(P9Narrative.clip.length + 1f));
        }
    }

    public void ProteinGrabHandler(GameObject grabbed)
    {
        grabbed.GetComponent<ProteinAudioScript>().PlayPing();

    }

    public void OnIntroVideoOver()
    {
        BackgroundMusic.Play();
        MembraneNarrative.PlayDelayed(1f);
        P0Narrative.PlayDelayed(7f);
    }

    IEnumerator PlayOutro(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        BackgroundMusic.Stop();
        GameObject.Find("GameManager").GetComponent<VideoScript>().PlayOutroVideo();
    }

}

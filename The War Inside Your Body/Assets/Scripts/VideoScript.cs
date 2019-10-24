﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{

    public bool PlayIntroVideo = false;
    public GameObject VideoCanvas;
    public GameObject IntroVideo;
    public GameObject OutroVideo;


    // Start is called before the first frame update
    void Start()
    {
        if(PlayIntroVideo)
        {
            VideoCanvas.SetActive(true);
            IntroVideo.SetActive(true);

            IntroVideo.GetComponent<UnityEngine.Video.VideoPlayer>().loopPointReached += IntroVideoOver;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IntroVideoOver(UnityEngine.Video.VideoPlayer vp)
    {
        VideoCanvas.SetActive(false);
        IntroVideo.SetActive(false);

        GameObject.Find("Audio Manager").GetComponent<AudioEventScript>().OnIntroVideoOver();

        GameObject protein = GameObject.Find("protein_Root_V");
        protein.GetComponent<Animator>().SetTrigger("playFlyIn");
    }

    void OutroVideoOver(UnityEngine.Video.VideoPlayer vp)
    {
        VideoCanvas.SetActive(false);
        OutroVideo.SetActive(false);

        //Application.Quit();
        SceneManager.LoadScene("SampleScene");
        
    }

    public void PlayOutroVideo()
    {
        VideoCanvas.SetActive(true);
        OutroVideo.SetActive(true);

        OutroVideo.GetComponent<UnityEngine.Video.VideoPlayer>().loopPointReached += OutroVideoOver;
    }
}

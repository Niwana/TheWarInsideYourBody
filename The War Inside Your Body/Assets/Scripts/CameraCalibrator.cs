using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCalibrator : MonoBehaviour
{
    public GameObject xPosSlider;
    public GameObject yPosSlider;
    public GameObject xScaleSlider;
    public GameObject yScaleSlider;

    // Start is called before the first frame update
    void Start()
    {
        //initialise sliders with the saved preferences
        Slider sliderComponent = xPosSlider.GetComponent<Slider>();
        sliderComponent.value = PlayerPrefs.HasKey("CameraXPos") ? PlayerPrefs.GetFloat("CameraXPos") : -10.0f;

        sliderComponent = yPosSlider.GetComponent<Slider>();
        sliderComponent.value = PlayerPrefs.HasKey("CameraYPos") ? PlayerPrefs.GetFloat("CameraYPos") : 10.0f;

        sliderComponent = xScaleSlider.GetComponent<Slider>();
        sliderComponent.value = PlayerPrefs.HasKey("CameraXScale") ? PlayerPrefs.GetFloat("CameraXScale") : 10.0f;

        sliderComponent = yScaleSlider.GetComponent<Slider>();
        sliderComponent.value = PlayerPrefs.HasKey("CameraYScale") ? PlayerPrefs.GetFloat("CameraYScale") : 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCameraSettings()
    {
        //Debug.Log("X SLider:" + xPosSlider.GetComponent<Slider>().value);

        PlayerPrefs.SetFloat("CameraXPos", xPosSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("CameraYPos", yPosSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("CameraXScale", xScaleSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("CameraYScale", yScaleSlider.GetComponent<Slider>().value);
    }
}

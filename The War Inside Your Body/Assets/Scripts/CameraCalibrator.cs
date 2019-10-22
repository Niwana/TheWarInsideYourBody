using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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

        float xPos0 = xPosSlider.GetComponent<Slider>().value;
        float yPos0 = yPosSlider.GetComponent<Slider>().value;
        float xScale = xScaleSlider.GetComponent<Slider>().value;
        float yScale = yScaleSlider.GetComponent<Slider>().value;

        //save prefs
        PlayerPrefs.SetFloat("CameraXPos", xPos0);
        PlayerPrefs.SetFloat("CameraYPos", yPos0);
        PlayerPrefs.SetFloat("CameraXScale", xScale);
        PlayerPrefs.SetFloat("CameraYScale", yScale);
        PlayerPrefs.Save(); //so it saves before quitting

    }

    public void OnDrawGizmos()
    {
        float xPos0 = xPosSlider.GetComponent<Slider>().value;
        float yPos0 = yPosSlider.GetComponent<Slider>().value;
        float xPos1 = xPos0 + xScaleSlider.GetComponent<Slider>().value;
        float yPos1 = yPos0 - yScaleSlider.GetComponent<Slider>().value;

        //draw rectangle on world to visualise
        // Draws a blue rectangle to the webcam tracking target window
        Vector3 p1 = new Vector3(xPos0, 0, yPos0);
        Vector3 p2 = new Vector3(xPos1, 0, yPos0);
        Vector3 p3 = new Vector3(xPos1, 0, yPos1);
        Vector3 p4 = new Vector3(xPos0, 0, yPos1);

        //Handles.DrawBezier(p1, p2, p1, p2, Color.red, null, 10f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}

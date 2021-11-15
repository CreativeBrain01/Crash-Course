using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorController : MonoBehaviour
{
    [SerializeField]
    Slider redSlider;
    [SerializeField]
    Image redSliderBG;

    [SerializeField]
    Slider greenSlider;
    [SerializeField]
    Image greenSliderBG;

    [SerializeField]
    Slider blueSlider;
    [SerializeField]
    Image blueSliderBG;


    [SerializeField]
    RawImage characterIMG;

    void Start()
    {
        characterIMG.texture = VehicleHandler.VehicleToSprite((VehicleHandler.eVehicle)PlayerPrefs.GetInt("vehicle")).texture;
        characterIMG.SetNativeSize();

        redSlider.value = PlayerPrefs.GetFloat("color-Red", 1);
        greenSlider.value = PlayerPrefs.GetFloat("color-Green", 1);
        blueSlider.value = PlayerPrefs.GetFloat("color-Blue", 1);
    }

    void Update()
    {
        characterIMG.color = new Color(redSlider.value, greenSlider.value, blueSlider.value);

        UpdateSliderColors();
    }

    void UpdateSliderColors()
    {
        Color redColor = new Color(redSlider.value, 0, 0);
        Color greenColor = new Color(0, greenSlider.value, 0);
        Color blueColor = new Color(0, 0, blueSlider.value);

        redSlider.fillRect.gameObject.GetComponent<Image>().color = redColor;
        redSlider.handleRect.gameObject.GetComponent<Image>().color = redColor;

        greenSlider.fillRect.gameObject.GetComponent<Image>().color = greenColor;
        greenSlider.handleRect.gameObject.GetComponent<Image>().color = greenColor;

        blueSlider.fillRect.gameObject.GetComponent<Image>().color = blueColor;
        blueSlider.handleRect.gameObject.GetComponent<Image>().color = blueColor;
    }

    public void UpdateRed(float r)
    {
        PlayerPrefs.SetFloat("color-Red", r);
    }

    public void UpdateGreen(float g)
    {
        PlayerPrefs.SetFloat("color-Green", g);
    }

    public void UpdateBlue(float b)
    {
        PlayerPrefs.SetFloat("color-Blue", b);
    }
}
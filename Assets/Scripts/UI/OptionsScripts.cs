using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScripts : MonoBehaviour {

    public Dropdown dropdownRes, dropdownGFX;
    public int resolutionX, resolutionY;
    public bool fullscreen = false;

    public void SetResolution()
    {
        Debug.Log(dropdownRes.value);
        switch (dropdownRes.value)
        {
            case 0:
                resolutionX = 1920;
                resolutionY = 1080;
                Screen.SetResolution(resolutionX, resolutionY, fullscreen);
                break;

            case 1:
                resolutionX = 1600;
                resolutionY = 900;
                Screen.SetResolution(resolutionX, resolutionY, fullscreen);
                break;

            case 2:
                resolutionX = 1280;
                resolutionY = 720;
                Screen.SetResolution(resolutionX, resolutionY, fullscreen);
                break;

            case 3:
                resolutionX = 720;
                resolutionY = 480;
                Screen.SetResolution(resolutionX, resolutionY, fullscreen);
                break;
        }

        PlayerPrefs.SetInt("resolution", dropdownRes.value);

    }

    public void SetGFXQuality()
    {
        Debug.Log(dropdownGFX.value);

        PlayerPrefs.SetInt("graphicsQuality", dropdownGFX.value);
    }

    public void ToggleFullscreenOn()
    {
        fullscreen = true;
        Debug.Log(fullscreen);
        Screen.SetResolution(resolutionX, resolutionY, fullscreen);

        PlayerPrefs.SetInt("fullscreen", 1);
    }


    public void ToggleFullscreenOff()
    {
        fullscreen = false;
        Debug.Log(fullscreen);
        Screen.SetResolution(resolutionX, resolutionY, fullscreen);

        PlayerPrefs.SetInt("fullscreen", 0);
    }

    public void Start()
    {
        dropdownRes.value = PlayerPrefs.GetInt("resolution");
        dropdownGFX.value = PlayerPrefs.GetInt("graphicsQuality");
        switch (PlayerPrefs.GetInt("fullscreen"))
        {
            case 0:
                fullscreen = false;
                break;
            case 1:
                fullscreen = true;
                break;
        }

    }
}

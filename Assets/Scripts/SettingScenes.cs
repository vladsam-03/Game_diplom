using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingScenes : MonoBehaviour
{
    public ColorBSC colorBSC;
    public GameObject mainCamera;
    public Slider SliderBrightness;
    public Slider SliderContrast;
    public Slider SliderSaturation;
    

    public void GameScene()
    {
        SceneManager.LoadScene("3D_Scene 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartScane()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetBrightness(float brightness)
    {
        colorBSC.brightness = brightness * 1;
    }
    public void SetContrast(float contrast)
    {
        colorBSC.contrast = contrast * 1;
    }
    public void SetSaturation(float saturation)
    {
        colorBSC.saturation = saturation * 1;
    }
    public void DefaultColor()
    {
        SliderBrightness.value = 1;
        SliderContrast.value = 1;
        SliderSaturation.value = 1;
    }

}

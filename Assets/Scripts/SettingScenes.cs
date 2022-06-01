using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingScenes : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("3D_Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartScane()
    {
        SceneManager.LoadScene("Menu");
    }
}

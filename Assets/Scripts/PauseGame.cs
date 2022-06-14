using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private GameObject settings;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (heroPanel.activeSelf)
            {
                heroPanel.SetActive(false);
            }
            else if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else if (!menu.activeSelf)
            {
                if (settings.activeSelf)
                {
                    settings.SetActive(false);
                }
                    menu.SetActive(true);
            }
        }

    }
}

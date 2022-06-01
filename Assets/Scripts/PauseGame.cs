using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject heroPanel;
    private bool isHeroPanelActive;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (var panel in panels)
            {
            if (panel.activeSelf)
                panel.SetActive(false);
            else
                panel.SetActive(true);
            }
            if (heroPanel.activeSelf)
            {
                isHeroPanelActive = true;
                heroPanel.SetActive(false);
            }
            else if (isHeroPanelActive)
            {
                isHeroPanelActive = false;
                heroPanel.SetActive(true);
            }

        }

    }
}

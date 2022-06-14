using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public List<Tile> Tiles = new List<Tile>();
    public List<GameObject> Borders = new List<GameObject>();
    public GameObject Fog;
    public GameObject FogLight;
    public bool IsVisited;

    private void Update()
    {
        foreach (var tile in Tiles)
        {
            if (tile.CurrentPerson != null)
            {
                FogLight.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionControl : MonoBehaviour
{
    [SerializeField] private List<Region> regions = new List<Region>();

    public void ShadowRegions()
    {
        foreach (var reg in regions)
        {
            if (reg.IsVisited)
            {

                List<Tile> PlayerTiles = new List<Tile>();
                foreach (var tile in reg.Tiles)
                {
                    if (tile.CurrentPerson != null)
                    {
                        PlayerTiles.Add(tile);
                    }
                }
                if (PlayerTiles.Count == 0)
                {
                    reg.FogLight.SetActive(true);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Hero
{
    public Tile Target;
    public bool step;
    public Tile LastTile;
    public Tile LastLastTile;
    public Tile CurrentTarget;
    public EnemyWalkType WalkType;
    public List<Tile> Zone = new List<Tile>();
    public int IndexTileZone = 1;
    public bool IsReturn = true;
}
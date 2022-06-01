using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Hero
{
    public Tile Target;
    public bool step;
    public GameObject LastTile;
    public GameObject CurrentTarget;

}
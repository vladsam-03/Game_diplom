using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int ID;
    public bool CanStep;
    public bool CanUse;
    public bool IsExploredPlayer;
    public bool IsExploredEnemy;
    public Biom Biom;
    public bool Last;

}

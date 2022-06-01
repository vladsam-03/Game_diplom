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
    public bool IsAttackPlayer;
    public bool IsAttackEnemy;
    public Biom Biom;
    public Hero CurrentPerson; //новое
    public bool Last;

}

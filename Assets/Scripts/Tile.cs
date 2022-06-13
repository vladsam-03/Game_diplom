using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int ID;
    public bool CanStep;
    public bool CanUse;
    public bool IsExploredPlayer;
    public bool IsAttackPlayer;
    public bool IsAttackEnemy;
    public Region Region;
    public Hero CurrentPerson; 
    public bool Last;

}

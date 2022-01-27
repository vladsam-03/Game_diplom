using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biom : MonoBehaviour
{
    public int TileCount = 81;
    public int barrierCount = 9;
    public TypeBiome TypeBiome;
    public List<Tile> Tiles; //В зависимости от биома свои тайлы
    public List<Tile> TilesGameObject;
}
public enum TypeBiome
{
    Electro, Field, Fire, Forest, Rook, Snow, Village, Water, City
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnWorld : MonoBehaviour
{
    public List<Biom> Bioms;
    public List<Player> Heroes;
    public List<Enemy> Enemies;
    public GameObject Spawn;
    private GameObject PlayerSpawn;
    private GameObject EnemySpawn;
    public static Hero SelectedHero;
    public GameObject Map;
    private SelectHero selectHero;

    void Start()
    {
        selectHero = GetComponent<SelectHero>();

        #region Results
        List<int> resultBiom = new List<int>(); //Биомы
        List<int> resultBiomXYZ = new List<int>(); //Координаты биомов
        List<int> resultLayout = new List<int>(); //Разметка
        List<int> resultLayoutXYZ = new List<int>(); //Координаты разметки
        List<int> resultSpawnPlayer = new List<int>(); //выбор места спавна NPC
        List<int> resultSpawnEnemy = new List<int>(); //выбор места спавна NPC
        #endregion
        #region Spawn tiels
        float biomX = 1000;
        float biomZ = 1000;
        float SpawnPlayerX = 0;
        float SpawnPlayerZ = 0;
        float SpawnEnemyX = 0;
        float SpawnEnemyZ = 0;


        for (int j = 0; j < Bioms.Count; j++)
        {
            Dictionary<int, Tile> AllTiles = new Dictionary<int, Tile>();
            int indexbiom = Random.Range(0, Bioms.Count);
            while (resultBiom.Exists(s => s == indexbiom)) //Биомы
            {
                indexbiom = Random.Range(0, Bioms.Count);
            }
            resultBiom.Add(indexbiom);
            var Biome = Bioms[indexbiom];
            GameObject SelectedBiome = Instantiate(Biome.gameObject, new Vector3(biomX, 0, biomZ), Quaternion.identity, Map.transform);

            float LayoutX = SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15;
            float LayoutZ = SelectedBiome.transform.position.z - SelectedBiome.transform.lossyScale.z / 2 + 15;

            for (int i = 0; i < Biome.TileCount; i++)
            {
                var Tile = Biome.Tiles[0];

                Tile СreatedTile = Instantiate(Tile, new Vector3(LayoutX, 1, LayoutZ), Quaternion.identity, Map.transform);
                СreatedTile.gameObject.transform.position = new Vector3(LayoutX, 1, LayoutZ);
                СreatedTile.ID = i;
                СreatedTile.Biom = SelectedBiome.GetComponent<Biom>();
                AllTiles.Add(СreatedTile.ID, СreatedTile);
                SelectedBiome.GetComponent<Biom>().TilesGameObject.Add(СreatedTile);
                LayoutX += 30;
                if (LayoutX == SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15 + 270)
                {
                    LayoutX = SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15;
                    LayoutZ += 30;
                }
            }
            #region переспавн блокируюших тайлов 
            var BarrierTileIndex = 0;
            var BarrierTile = Biome.Tiles[1];
            var LastBarrierTileIndex = 0;

            for (int i = 0; i < 9; i++)
            {
                var PartsBiome = Random.Range(0, 3);
                if (resultBiom.Count == 2)
                {
                    if (i == 0 && PartsBiome == 1)
                    {
                        PartsBiome = Random.Range(0, 2);
                        if (PartsBiome == 1)
                        {
                            PartsBiome = 2;
                        }
                    }
                }
                if (resultBiom.Count == 8)
                {
                    if (i == 8 && PartsBiome == 1)
                    {
                        PartsBiome = Random.Range(0, 2);
                        if (PartsBiome == 1)
                        {
                            PartsBiome = 2;
                        }
                    }
                }
                if (PartsBiome == 0)
                {
                    BarrierTileIndex = Random.Range(0, 3);
                }
                else if (PartsBiome == 1)
                {
                    BarrierTileIndex = Random.Range(3, 6);
                }
                else if (PartsBiome == 2)
                {
                    BarrierTileIndex = Random.Range(6, 9);
                }
                var DeleteTile = AllTiles.First( s => s.Key == (BarrierTileIndex + (9 * i)));
                AllTiles.Remove(BarrierTileIndex + (9 * i));
                var NewBarrierTile = Instantiate(BarrierTile, DeleteTile.Value.gameObject.transform.position, Quaternion.identity, Map.transform);
                NewBarrierTile.ID = DeleteTile.Key;
                NewBarrierTile.Biom = DeleteTile.Value.Biom;
                AllTiles.Add(BarrierTileIndex + (9 * i), NewBarrierTile);
                Destroy(DeleteTile.Value.gameObject);
                LastBarrierTileIndex = BarrierTileIndex;
            }
            #endregion

            biomX += 270;
            if (biomX == 1810)
            {
                biomX = 1000;
                biomZ += 270;
            }

            if (resultBiom.Count == 2)
            {
                SpawnPlayerX = SelectedBiome.transform.position.x;
                SpawnPlayerZ = SelectedBiome.transform.position.z;
                foreach (var tile in SelectedBiome.GetComponent<Biom>().TilesGameObject)
                {
                    tile.IsExploredEnemy = false;
                    tile.IsExploredPlayer = true;
                }
            }
            else if (resultBiom.Count == 8)
            {
                SpawnEnemyX = SelectedBiome.transform.position.x;
                SpawnEnemyZ = SelectedBiome.transform.position.z;
                foreach (var tile in SelectedBiome.GetComponent<Biom>().TilesGameObject)
                {
                    tile.IsExploredEnemy = true;
                    tile.IsExploredPlayer = false;
                }
            }
        }
        PlayerSpawn = Instantiate(Spawn, new Vector3(SpawnPlayerX, 0, SpawnPlayerZ - 150), Quaternion.identity, Map.transform);
        EnemySpawn = Instantiate(Spawn, new Vector3(SpawnEnemyX, 0, SpawnEnemyZ + 150), Quaternion.Euler(0, 180, 0), Map.transform);
        #endregion
        #region Spawn Npc-Player
        int[] SpawnCoord = { -30, 0, 30 };
        float rotation = 180;

        for (int i = 0; i < 3; i++)
        {
            Player player = Instantiate(Heroes[i], new Vector3(PlayerSpawn.transform.position.x + SpawnCoord[i], PlayerSpawn.transform.position.y, PlayerSpawn.transform.position.z), Quaternion.identity);
            player.name = Heroes[i].Name + " Player";
            if (selectHero.Players == null)
                selectHero.Players = new List<Player>();
            player.IsCanMove = true;
            selectHero.Players.Add(player);

            Enemy enemy  = Instantiate(Enemies[i], new Vector3(EnemySpawn.transform.position.x + SpawnCoord[i], EnemySpawn.transform.position.y, EnemySpawn.transform.position.z), Quaternion.Euler(0, rotation, 0));
            enemy.name = Enemies[i].Name + " Enemy";
            if (MoveBot.Bots == null)
                MoveBot.Bots = new List<GameObject>();  
            MoveBot.Bots.Add(enemy.gameObject);
        }

        #endregion

    }
}
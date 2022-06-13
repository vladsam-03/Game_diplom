using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Помойка : MonoBehaviour
{
    #region спавн построек
    /*public GameObject platform;// ^ Для построек (старая версия)
    var scale = platform.transform.lossyScale;
    for (int i = 0; i < 3; i++)
    {
        int index = Random.Range(0, 3);
        var prefab = Bilding[index];
        var scaleprefab = prefab.transform.lossyScale;
        if (index == 0)
        {
            scaleprefab.x += 30;
        }
        Instantiate(prefab, new Vector3(Random.Range(((-scale.x / 2) + scaleprefab.x / 2) + scaleprefab.x, ((scale.x / 2) - scaleprefab.x / 2) - scaleprefab.x), 1, Random.Range(((-scale.z / 2) + scaleprefab.z / 2) + scaleprefab.z, ((scale.z / 2) - scaleprefab.z / 2) - scaleprefab.z)), Quaternion.identity);
    }*/
    #endregion
    #region спавн биомов и тайлов
    /*float biomX = 0;
    float biomZ = 0;
    float SpawnPlayerX = 0;
    float SpawnPlayerZ = 0;
    float SpawnEnemyX = 0;
    float SpawnEnemyZ = 0;

        for (int j = 0; j<Bioms.Count; j++) // Биомы
        {
            int indexbiom = Random.Range(0, Bioms.Count);
            while (resultBiom.Exists(s => s == indexbiom)) 
            {
                indexbiom = Random.Range(0, Bioms.Count);
            }
resultBiom.Add(indexbiom);
            var Biom = Bioms[indexbiom];
GameObject SelectedBiom = Instantiate(Biom.gameObject, new Vector3(biomX, 0, biomZ), Quaternion.identity);


float LayoutX = SelectedBiom.transform.position.x - SelectedBiom.transform.lossyScale.x; // Тайлы
float LayoutZ = SelectedBiom.transform.position.z - SelectedBiom.transform.lossyScale.z;
            for (int i = 0; i<Biom.TileCount; i++)
            {
                int indexrotationxyz = Random.Range(0, 4);
float y = 0;

                if (indexrotationxyz == 1) //Поворот разметки
                    y = 90;
                else if (indexrotationxyz == 2)
                    y = 180;
                else if (indexrotationxyz == 3)
                    y = 270;
                Instantiate(Layout, new Vector3(LayoutX, 1, LayoutZ), Quaternion.Euler(0, y, 0));
                LayoutX += 90;
                if (LayoutX > SelectedBiom.transform.position.x - SelectedBiom.transform.lossyScale.x / 3 + 180)
                {
                    LayoutX = SelectedBiom.transform.position.x - SelectedBiom.transform.lossyScale.x / 3;
                    LayoutZ += 30;
                }
            }

            biomX += 30;
            if (biomX == 810)
            {
                biomX = 0;
                biomZ += 30;
            }

            if (resultBiom.Count == 2)
            {
                SpawnPlayerX = SelectedBiom.transform.position.x;
                SpawnPlayerZ = SelectedBiom.transform.position.z;
            }
            if (resultBiom.Count == 8)
            {
                SpawnEnemyX = SelectedBiom.transform.position.x;
                SpawnEnemyZ = SelectedBiom.transform.position.z;
            }
        }
        PlayerSpawn = Instantiate(Spawn, new Vector3(SpawnPlayerX, 0, SpawnPlayerZ - 150), Quaternion.identity);
        EnemySpawn = Instantiate(Spawn, new Vector3(SpawnEnemyX, 0, SpawnEnemyZ + 150), Quaternion.Euler(0, 180, 0));*/
    #endregion
    #region спавн тайлов

    /*int indexTile = Random.Range(0, Biome.Tiles.Count);
    var Tile = Biome.Tiles[indexTile];
        if (Biome.barrierCount != BarrierCount && Tile.canStep == false)
            {
                BarrierCount++;
            }
        else if (Biome.barrierCount == BarrierCount && Tile.canStep == false)
            {
                var EmptyTiles = Biome.Tiles.FindAll(s => s.canStep == true);
                indexTile = Random.Range(0, EmptyTiles.Count);
                Tile = EmptyTiles[indexTile];
            }
        Instantiate(Tile, new Vector3(LayoutX, 1, LayoutZ), Quaternion.identity);
        LayoutX += 30;
        if (LayoutX == SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15 + 270)
            {
                LayoutX = SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15;
                LayoutZ += 30;
            }*/


    /*//перереспавн если барьеров достаточно тайлов
    if (Biome.barrierCount != BarrierCount && Tile.canStep == false)
    {
        BarrierCount++;
    }
    else if (Biome.barrierCount == BarrierCount && Tile.canStep == false)
    {
        var EmptyTiles = Biome.Tiles.FindAll(s => s.canStep == true);
        indexTile = Random.Range(0, EmptyTiles.Count);
        Tile = EmptyTiles[indexTile];
    }*/

    #endregion
    #region чёрный лист
    /*List<Tile> BlackTileList = new List<Tile>();
                    foreach (var hero in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (hero.gameObject.name.ToLower().Contains("player"))
                        {
                            if (hero.GetComponent<Hero>().SelectTile == null)
                            {
                                Ray ray1 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.up * -30);
    RaycastHit raycastHit;
                                if (Physics.Raycast(ray1, out raycastHit))
                                {
                                    hero.GetComponent<Hero>().SelectTile = raycastHit.collider.gameObject.GetComponent<Tile>();
                                }
                            }
                            BlackTileList.Add(hero.GetComponent<Hero>().SelectTile);
                        }
                    }

                    if (!BlackTileList.Exists(s => s.transform.position == hit.collider.gameObject.GetComponent<Tile>().transform.position))
                    {}
     для выделения тайла под обьектом
            if (transform.position == hit.collider.gameObject.transform.position)
            {
                step = false;
                Ray ray = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.up * -30);
RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit))
                {
                    GetComponent<Hero>().SelectTile = raycastHit.collider.gameObject.GetComponent<Tile>();
                }
            }*/
    #endregion
    #region старое движение ботов

    /*string[] EnemyBot = { "EnemyZombie", "EnemySkeleton", "EnemyVampire" };
    private int NumberMove = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Move(Button EndTurn)
    {
        EndTurn.interactable = false;
        for (int i = 0; i < 3; i++)
        {
            var EnemyStep = GameObject.Find(EnemyBot[i]).GetComponent<StepNPC>();
            if (NumberMove == 1)
            {
                EnemyStep.StepDown();
            }
            else
            {
                var RandomMoveBot = Random.Range(0, 4);
                switch (RandomMoveBot)
                {
                    case 0: EnemyStep.Move(EndTurn); EnemyStep.StepUp(); break;
                    case 1: EnemyStep.Move(EndTurn); EnemyStep.StepDown(); break;
                    case 2: EnemyStep.Move(EndTurn); EnemyStep.StepLeft(); break;
                    case 3: EnemyStep.Move(EndTurn); EnemyStep.StepRight(); break;
                }
            }
            NumberMove++;
        }
    }*/
    #endregion
    #region движение без лучей до таргета
    /*public void SetTarget()
    {
        if (SelectTile == null)
        {
            int rmd = Random.Range(3, 6);
            Ray ray = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 30, gameObject.transform.position.z - 30), gameObject.transform.up * -30);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                while (true)
                {
                    var tile = hit.collider.gameObject.GetComponent<Tile>().Biom.TilesGameObject[rmd];
                    if (tile.CanStep)
                    {
                        TargetBiom = tile;
                        break;
                    }
                }
            }
        }
    }
    private void Update()
    {
        if (!TargetBiom || gameObject.transform.position == TargetBiom.gameObject.transform.position)
        {
            SetTarget();
            MoveBot.Bots.RemoveAll(s => s.Name == Name);
            MoveBot.Bots.Add(this);
        }
        if (step)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetBiom.transform.position, Time.deltaTime * 100);
            transform.LookAt(TargetBiom.transform.position);
            if (transform.position == TargetBiom.transform.position)
            {
                step = false;
            }
        }
    }*/
    #endregion
    #region Spawn world

//public class SpawnWorld : MonoBehaviour
//{
//    public List<Biom> Bioms;
//    public List<Player> Heroes;
//    public List<Enemy> Enemies;
//    public GameObject Spawn;
//    private GameObject PlayerSpawn;
//    private GameObject EnemySpawn;
//    public static Hero SelectedHero;
//    public GameObject Map;
//    private SelectHero selectHero;

//    void Start()
//    {
//        selectHero = GetComponent<SelectHero>();

//        #region Results
//        List<int> resultBiom = new List<int>(); //Биомы
//        List<int> resultBiomXYZ = new List<int>(); //Координаты биомов
//        List<int> resultLayout = new List<int>(); //Разметка
//        List<int> resultLayoutXYZ = new List<int>(); //Координаты разметки
//        List<int> resultSpawnPlayer = new List<int>(); //выбор места спавна NPC
//        List<int> resultSpawnEnemy = new List<int>(); //выбор места спавна NPC
//        #endregion
//        #region Spawn tiels
//        float biomX = 1000;
//        float biomZ = 1000;
//        float SpawnPlayerX = 0;
//        float SpawnPlayerZ = 0;
//        float SpawnEnemyX = 0;
//        float SpawnEnemyZ = 0;


//        for (int j = 0; j < Bioms.Count; j++)
//        {
//            Dictionary<int, Tile> AllTiles = new Dictionary<int, Tile>();
//            int indexbiom = Random.Range(0, Bioms.Count);
//            while (resultBiom.Exists(s => s == indexbiom)) //Биомы
//            {
//                indexbiom = Random.Range(0, Bioms.Count);
//            }
//            resultBiom.Add(indexbiom);
//            var Biome = Bioms[indexbiom];
//            GameObject SelectedBiome = Instantiate(Biome.gameObject, new Vector3(biomX, 0, biomZ), Quaternion.identity, Map.transform);

//            float LayoutX = SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15;
//            float LayoutZ = SelectedBiome.transform.position.z - SelectedBiome.transform.lossyScale.z / 2 + 15;

//            for (int i = 0; i < Biome.TileCount; i++)
//            {
//                var Tile = Biome.Tiles[0];

//                Tile СreatedTile = Instantiate(Tile, new Vector3(LayoutX, 1, LayoutZ), Quaternion.identity, Map.transform);
//                СreatedTile.gameObject.transform.position = new Vector3(LayoutX, 1, LayoutZ);
//                СreatedTile.ID = i;
//                СreatedTile.Biom = SelectedBiome.GetComponent<Biom>();
//                AllTiles.Add(СreatedTile.ID, СreatedTile);
//                SelectedBiome.GetComponent<Biom>().TilesGameObject.Add(СreatedTile);
//                LayoutX += 30;
//                if (LayoutX == SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15 + 270)
//                {
//                    LayoutX = SelectedBiome.transform.position.x - SelectedBiome.transform.lossyScale.x / 2 + 15;
//                    LayoutZ += 30;
//                }
//            }
//            #region переспавн блокируюших тайлов 
//            var BarrierTileIndex = 0;
//            var BarrierTile = Biome.Tiles[1];
//            var LastBarrierTileIndex = 0;

//            for (int i = 0; i < 9; i++)
//            {
//                var PartsBiome = Random.Range(0, 3);
//                if (resultBiom.Count == 2)
//                {
//                    if (i == 0 && PartsBiome == 1)
//                    {
//                        PartsBiome = Random.Range(0, 2);
//                        if (PartsBiome == 1)
//                        {
//                            PartsBiome = 2;
//                        }
//                    }
//                }
//                if (resultBiom.Count == 8)
//                {
//                    if (i == 8 && PartsBiome == 1)
//                    {
//                        PartsBiome = Random.Range(0, 2);
//                        if (PartsBiome == 1)
//                        {
//                            PartsBiome = 2;
//                        }
//                    }
//                }
//                if (PartsBiome == 0)
//                {
//                    BarrierTileIndex = Random.Range(0, 3);
//                }
//                else if (PartsBiome == 1)
//                {
//                    BarrierTileIndex = Random.Range(3, 6);
//                }
//                else if (PartsBiome == 2)
//                {
//                    BarrierTileIndex = Random.Range(6, 9);
//                }
//                var DeleteTile = AllTiles.First( s => s.Key == (BarrierTileIndex + (9 * i)));
//                AllTiles.Remove(BarrierTileIndex + (9 * i));
//                var NewBarrierTile = Instantiate(BarrierTile, DeleteTile.Value.gameObject.transform.position, Quaternion.identity, Map.transform);
//                NewBarrierTile.ID = DeleteTile.Key;
//                NewBarrierTile.Biom = DeleteTile.Value.Biom;
//                AllTiles.Add(BarrierTileIndex + (9 * i), NewBarrierTile);
//                Destroy(DeleteTile.Value.gameObject);
//                LastBarrierTileIndex = BarrierTileIndex;
//            }
//            #endregion

//            biomX += 270;
//            if (biomX == 1810)
//            {
//                biomX = 1000;
//                biomZ += 270;
//            }

//            if (resultBiom.Count == 2)
//            {
//                SpawnPlayerX = SelectedBiome.transform.position.x;
//                SpawnPlayerZ = SelectedBiome.transform.position.z;
//                foreach (var tile in SelectedBiome.GetComponent<Biom>().TilesGameObject)
//                {
//                    tile.IsExploredEnemy = false;
//                    tile.IsExploredPlayer = true;
//                }
//            }
//            else if (resultBiom.Count == 8)
//            {
//                SpawnEnemyX = SelectedBiome.transform.position.x;
//                SpawnEnemyZ = SelectedBiome.transform.position.z;
//                foreach (var tile in SelectedBiome.GetComponent<Biom>().TilesGameObject)
//                {
//                    tile.IsExploredEnemy = true;
//                    tile.IsExploredPlayer = false;
//                }
//            }
//        }
//        PlayerSpawn = Instantiate(Spawn, new Vector3(SpawnPlayerX, 0, SpawnPlayerZ - 150), Quaternion.identity, Map.transform);
//        EnemySpawn = Instantiate(Spawn, new Vector3(SpawnEnemyX, 0, SpawnEnemyZ + 150), Quaternion.Euler(0, 180, 0), Map.transform);
//        #endregion
//        #region Spawn Npc-Player
//        int[] SpawnCoord = { -30, 0, 30 };
//        float rotation = 180;

//        for (int i = 0; i < 3; i++)
//        {
//            Player player = Instantiate(Heroes[i], new Vector3(PlayerSpawn.transform.position.x + SpawnCoord[i], PlayerSpawn.transform.position.y, PlayerSpawn.transform.position.z), Quaternion.identity);
//            player.name = Heroes[i].Name + " Player";
//            if (selectHero.Players == null)
//                selectHero.Players = new List<Player>();
//            player.IsCanMove = true;
//            selectHero.Players.Add(player);

//            Enemy enemy  = Instantiate(Enemies[i], new Vector3(EnemySpawn.transform.position.x + SpawnCoord[i], EnemySpawn.transform.position.y, EnemySpawn.transform.position.z), Quaternion.Euler(0, rotation, 0));
//            enemy.name = Enemies[i].Name + " Enemy";
//            if (MoveBot.Bots == null)
//                MoveBot.Bots = new List<GameObject>();  
//            MoveBot.Bots.Add(enemy.gameObject);
//        }

//        #endregion

//    }
//}
    #endregion
}

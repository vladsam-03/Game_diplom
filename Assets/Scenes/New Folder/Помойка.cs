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
}

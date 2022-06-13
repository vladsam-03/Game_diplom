using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MoveBot : MonoBehaviour
{
    public List<Enemy> Enemies = new List<Enemy>();
    private Camera MainCamera;
    private List<Tile> Tiles = new List<Tile>();
    [SerializeField] private float speed;
    private bool isMove;
    public Enemy CurrentBot;
    private SelectHero selectHero;


    private void Start()
    {
        MainCamera = Camera.main;
        selectHero = GetComponent<SelectHero>();
    }

    private void Update()
    {
        if (CurrentBot != null && isMove)
        {
            CurrentBot.transform.position = Vector3.MoveTowards(CurrentBot.transform.position, CurrentBot.GetComponent<Enemy>().CurrentTarget.transform.position, Time.deltaTime * speed);
            CurrentBot.transform.LookAt(CurrentBot.GetComponent<Enemy>().CurrentTarget.transform.position);
            if (CurrentBot.transform.position == CurrentBot.GetComponent<Enemy>().CurrentTarget.transform.position)
            {
                isMove = false;
            }
        }
    }

    public void StartMove()
    {
        StartCoroutine(StartTurn());
    }


    public IEnumerator StartTurn()
    {
        foreach (var item in selectHero.Players)
            item.IsCanMove = false;
        foreach (var bot in Enemies)
        {
            for (int i = (int)bot.MoveAP; i > 0; i--)
            {
                bot.MoveAP--;
                List<Tile> playerTiles = new List<Tile>();
                if (bot.CurrentTarget != null)
                {
                    if (bot.LastLastTile != null)
                    {
                        bot.LastLastTile.Last = false;
                    }
                    if (bot.LastTile != null)
                    {
                        bot.LastLastTile = bot.LastTile;
                    }
                    bot.LastTile = bot.CurrentTarget;
                    bot.LastTile.IsAttackPlayer = false;
                    bot.LastTile.Last = true;
                    bot.CurrentTarget.CanStep = true;
                }
                if (bot.WalkType == EnemyWalkType.Guard)
                {
                    foreach (var tile in bot.CurrentTarget.Region.Tiles)
                    {
                        if (tile.CurrentPerson != null)
                        {
                            playerTiles.Add(tile);
                        }
                    }
                    if (bot.CurrentTarget == bot.Zone[bot.IndexTileZone])
                    {
                        bot.IsReturn = true;
                        bot.Target = null;
                    }
                    if (playerTiles.Count > 0)
                    {
                        float min = float.MaxValue;
                        bot.IsReturn = false;

                        foreach (var tile in playerTiles)
                        {
                            float distance = Vector3.Distance(tile.transform.position, bot.CurrentTarget.transform.position);
                            if (distance < min)
                            {
                                min = distance;
                                bot.Target = tile;
                            }
                        }
                    }
                    else if (bot.Zone.Count > 0 && bot.Target == null)
                    {
                        bot.CurrentTarget = bot.Zone[bot.IndexTileZone];
                        if (bot.Zone.Count == bot.IndexTileZone)
                        {
                            bot.IndexTileZone = 0;
                        }
                    }
                    else if (!bot.IsReturn)
                    {
                        bot.Target = bot.Zone[bot.IndexTileZone++];
                    }
                }
                else if (bot.WalkType == EnemyWalkType.Сhaser)
                {

                }

                if (bot.Target != null)
                {
                    Tiles = new List<Tile>();
                    SetRay(bot);
                    if (Tiles.Count == 0)
                    {
                        break;
                    }
                    else if (Tiles.Count == 1)
                    {
                        Tiles[0].GetComponent<Tile>().Last = false;
                        bot.GetComponent<Enemy>().CurrentTarget = Tiles[0];
                    }
                    else
                    {
                        float min = float.MaxValue;

                        foreach (var tile in Tiles)
                        {
                            if (!tile.GetComponent<Tile>().Last)
                            {
                                float distance = Vector3.Distance(tile.transform.position, bot.GetComponent<Enemy>().Target.transform.position);
                                if (distance < min)
                                {
                                    min = distance;
                                    tile.GetComponent<Tile>().IsAttackPlayer = true;
                                    bot.GetComponent<Enemy>().CurrentTarget = tile;
                                }
                            }
                            else
                                tile.GetComponent<Tile>().Last = false;
                        }
                    }

                }
                if (bot.CurrentTarget != null)
                {
                    bot.CurrentTarget.CanStep = false;
                    CurrentBot = bot;
                    isMove = true;
                    while (true)
                    {
                        if (isMove == false)
                        {
                            break;
                        }
                        yield return null;
                    }
                }
            }
            bot.MoveAP = bot.MoveMaxAP;
        }
        foreach (var item in selectHero.Players)
        {
            item.IsCanMove = true;
            item.MoveAP += item.RegenerateMoveAP;
            item.AttackAP += item.RegenerateAttackAP;
            if (item.MoveAP > item.MoveMaxAP)
            {
                item.MoveAP = item.MoveMaxAP;
            }
        }
    }


    public void SetRay(Enemy gameObject)
    {
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    Ray ray = new Ray(new Vector3(gameObject.transform.position.x + 30, gameObject.transform.position.y + 30, gameObject.transform.position.z), gameObject.transform.up * -30);
                    DoAction(ray, gameObject);
                    break;
                case 1:
                    Ray ray1 = new Ray(new Vector3(gameObject.transform.position.x - 30, gameObject.transform.position.y + 30, gameObject.transform.position.z), gameObject.transform.up * -30);
                    DoAction(ray1, gameObject);
                    break;
                case 2:
                    Ray ray2 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 30, gameObject.transform.position.z + 30), gameObject.transform.up * -30);
                    DoAction(ray2, gameObject);
                    break;
                case 3:
                    Ray ray3 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 30, gameObject.transform.position.z - 30), gameObject.transform.up * -30);
                    //Пример дебага для луча 
                    //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 30, transform.position.z - 30), transform.up * -30, Color.red);
                    DoAction(ray3, gameObject);
                    break;
            }
        }
    }

    private void DoAction(Ray ray, Enemy bot)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Tile>() != null)
            {
                if (hit.collider.gameObject.GetComponent<Tile>().CanStep == true)
                {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    //if (bot.GetComponent<Enemy>().LastTile != null && tile.transform == bot.GetComponent<Enemy>().LastTile.transform)
                    //{
                    //    tile.GetComponent<Tile>().Last = true;
                    //}
                    Tiles.Add(tile);

                }
            }
        }
    }

    public void RemoveBot(Enemy bot)
    {
        Enemies.Remove(bot);
    }

}


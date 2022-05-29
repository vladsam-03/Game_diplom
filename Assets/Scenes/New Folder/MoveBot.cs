using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MoveBot : MonoBehaviour
{
    public static List<GameObject> Bots;
    private Camera MainCamera;
    private List<GameObject> Tiles = new List<GameObject>();
    [SerializeField] private float speed;
    private bool isMove;
    public GameObject CurrentBot;


    private void Start()
    {
        MainCamera = Camera.main;
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
        //GetComponent<Hero>().AP == 3; //после нажатия на кнопку ап героев игрока должно возврашяться
    }


    public IEnumerator StartTurn()
    {
        foreach (var bot in Bots)
        {
            if (bot.GetComponent<Enemy>().CurrentTarget == null)
            {
                SetFirstTarget(bot);
            }
            for (int i = (int)bot.GetComponent<Hero>().AP; i > 0; i--)
            {
                //if (Vector3.Distance(bot.transform.position, ) == 30) // атаку сделать не смог)
                //{
                //    hit.collider.gameObject.GetComponent<Enemy>().HP -= bot.GetComponent<Enemy>().Attack;
                //}
                if (bot.GetComponent<Enemy>().CurrentTarget != null && bot.GetComponent<Enemy>().CurrentTarget.transform == bot.GetComponent<Enemy>().Target.transform)
                {
                    SetTarget(bot);
                }
                bot.GetComponent<Hero>().AP--;
                Tiles = new List<GameObject>();
                SetRay(bot);
                if (bot.GetComponent<Enemy>().Target.IsExploredEnemy == false)
                {// возможно здесь сделать так чтобы получать id биома и делать весь биом true
                    bot.GetComponent<Enemy>().Target.IsExploredEnemy = true;
                }
                else
                {
                    if (bot.GetComponent<Enemy>().CurrentTarget != null)
                    {
                        bot.GetComponent<Enemy>().LastTile = bot.GetComponent<Enemy>().CurrentTarget;
                        bot.GetComponent<Enemy>().LastTile.GetComponent<Tile>().IsAttackPlayer = false;
                        bot.GetComponent<Enemy>().CurrentTarget.GetComponent<Tile>().CanStep = true;
                    }
                    if (Tiles.Count == 0)
                    {
                        break;
                    }
                    else if (Tiles.Count == 1 && Tiles[0].GetComponent<Tile>().IsExploredEnemy)
                    {
                        Tiles[0].GetComponent<Tile>().Last = false;
                        bot.GetComponent<Enemy>().CurrentTarget = Tiles[0];
                    }
                    else
                    {
                        float min = float.MaxValue;

                        foreach (var tile in Tiles)
                        {
                            if (!tile.GetComponent<Tile>().Last && tile.GetComponent<Tile>().IsExploredEnemy)
                            {
                                float distance = Vector3.Distance(tile.transform.position, bot.GetComponent<Enemy>().Target.transform.position);
                                if (distance < min)
                                {
                                    min = distance;
                                    tile.GetComponent<Tile>().IsAttackPlayer = true;
                                    bot.GetComponent<Enemy>().CurrentTarget = tile;
                                }
                            }
                            else if (tile.GetComponent<Tile>().Last)
                                tile.GetComponent<Tile>().Last = false;
                        }
                    }
                    bot.GetComponent<Enemy>().CurrentTarget.GetComponent<Tile>().CanStep = false;
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
            bot.GetComponent<Hero>().AP = 3;
        }
    }

    private void SetTarget(GameObject bot)
    {
        SetRay(bot);
        foreach (var tile in Tiles)
        {
            if (tile.GetComponent<Tile>().IsExploredEnemy == false)
            {
                bot.GetComponent<Enemy>().Target = tile.GetComponent<Tile>();
            }
        }
    }

    private void SetFirstTarget(GameObject bot)
    {
        int rmd = UnityEngine.Random.Range(3, 6);
        Ray ray = new Ray(new Vector3(bot.transform.position.x, bot.transform.position.y + 30, bot.transform.position.z - 30), bot.transform.up * -30);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            foreach (var tile in hit.collider.gameObject.GetComponent<Tile>().Biom.TilesGameObject)
            {
                tile.IsExploredEnemy = true;
            }
            while (true)
            {
                var tile = hit.collider.gameObject.GetComponent<Tile>().Biom.TilesGameObject[rmd];
                if (tile.CanStep)
                {
                    bot.GetComponent<Enemy>().Target = tile;
                    break;
                }
            }
        }
    }

    public void SetRay(GameObject gameObject)
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

    private void DoAction(Ray ray, GameObject bot)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Tile>() != null)
            {
                if (hit.collider.gameObject.GetComponent<Tile>().CanStep == true)
                {
                    GameObject tile = hit.collider.gameObject;
                    if (bot.GetComponent<Enemy>().LastTile != null && tile.transform == bot.GetComponent<Enemy>().LastTile.transform)
                    {
                        tile.GetComponent<Tile>().Last = true;
                    }
                    Tiles.Add(tile);

                }
            }
        }
    }

    public void RemoveBot(GameObject bot)
    {
        Bots.Remove(bot);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPanel : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    [SerializeField] private GameObject Panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Panel.activeSelf)
            {
                Panel.SetActive(false);
            }
            else
                Panel.SetActive(true);

        }
    }

    public void UpHP()
    {
        foreach (var item in players)
        {
            item.GetComponent<Hero>().HP = 100;
            item.GetComponent<Hero>().MaxHP = 100;
        }
    }
    public void UpMoveAP()
    {
        foreach (var item in players)
        {
            item.GetComponent<Hero>().MoveAP = 100;
            item.GetComponent<Hero>().MoveMaxAP = 100;
        }
    }
    public void UpAttackAP()
    {
        foreach (var item in players)
        {
            item.GetComponent<Hero>().AttackAP = 100;
            item.GetComponent<Hero>().AttackMaxAP = 100;
        }
    }
    public void TeleportToSmallVillage()
    {
        players[0].transform.position = new Vector3(-540, 1, 1530);
        players[1].transform.position = new Vector3(-510, 1, 1530);
        players[2].transform.position = new Vector3(-480, 1, 1530);
    }
    public void TeleportToBigVillage()
    {
        players[0].transform.position = new Vector3(1140, 1, 1320);
        players[1].transform.position = new Vector3(1110, 1, 1320);
        players[2].transform.position = new Vector3(1080, 1, 1320);
    }
}

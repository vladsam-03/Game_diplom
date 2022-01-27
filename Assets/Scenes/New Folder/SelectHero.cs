using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHero : MonoBehaviour
{
    private Camera MainCamera;
    public GameObject Panel;
    public GameObject Porstrait;
    private List<GameObject> Tiles = new List<GameObject>();
    public static GameObject SelectedHero;

    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Player>() != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                        SelectedHero = hit.collider.gameObject;
                        if (Tiles.Count > 0)
                        {
                            foreach (var tile in Tiles)
                            {
                                tile.GetComponent<Tile>().CanUse = false;
                                tile.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                                tile.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                                tile.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                                tile.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                            }
                            Tiles = new List<GameObject>();
                        }
                        SetRay(hit.collider.gameObject);
                        Panel.SetActive(true);
                        Porstrait.SetActive(true);
                        Stats();
                        MoveCameraToPlayer();
                }
                else if (hit.collider.gameObject.GetComponent<Tile>() != null && hit.collider.gameObject.GetComponent<Tile>().CanUse == true && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    if (SelectedHero.GetComponent<Player>().SelectTile != null)
                    {
                        SelectedHero.GetComponent<Player>().SelectTile.CanStep = true;
                    }
                    SelectedHero.GetComponent<StepNPCNew>().step = true;
                    hit.collider.gameObject.GetComponent<Tile>().CanStep = false;
                    SelectedHero.GetComponent<Player>().SelectTile = hit.collider.gameObject.GetComponent<Tile>();
                    DeselectTile();
                    StartCoroutine("WaitNewRay");
                    Panel.SetActive(true);
                    Porstrait.SetActive(true);
                }
                else
                {
                    DeselectTile();
                }
            }
            else
            {
                DeselectTile();
            }
        }
        if(SelectedHero != null && SelectedHero.GetComponent<StepNPCNew>().step)
            MoveCameraToPlayer();
    }

    private void MoveCameraToPlayer()
    {
        if (SelectedHero.transform.rotation.eulerAngles.y == 0) // ¬ÔÂ∏‰
        {
            Porstrait.transform.position = new Vector3(SelectedHero.transform.position.x, SelectedHero.transform.position.y + 25, SelectedHero.transform.position.z + 25);
            Porstrait.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (SelectedHero.transform.rotation.eulerAngles.y == 90 || SelectedHero.transform.rotation.eulerAngles.y == -270) // ¬Ô‡‚Ó
        {
            Porstrait.transform.position = new Vector3(SelectedHero.transform.position.x + 25, SelectedHero.transform.position.y + 25, SelectedHero.transform.position.z);
            Porstrait.transform.rotation = Quaternion.Euler(0, 270, 0);

        }
        else if (SelectedHero.gameObject.transform.rotation.eulerAngles.y == 180) //Õ‡Á‡‰
        {
            Porstrait.transform.position = new Vector3(SelectedHero.transform.position.x, SelectedHero.transform.position.y + 25, SelectedHero.transform.position.z - 25);
            Porstrait.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (SelectedHero.transform.rotation.eulerAngles.y == -90 || SelectedHero.transform.rotation.eulerAngles.y == 270) //¬ÎÂ‚Ó
        {
            Porstrait.transform.position = new Vector3(SelectedHero.transform.position.x - 25, SelectedHero.transform.position.y + 25, SelectedHero.transform.position.z);
            Porstrait.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Stats()
    {
        Panel.transform.GetChild(0).gameObject.GetComponent<Text>().text = SelectedHero.GetComponent<Hero>().Name; //Œƒ ƒŒ¡¿¬»“‹ Œ¡ﬂ«¿“≈À‹ÕŒ
        Panel.transform.GetChild(0).gameObject.GetComponent<Text>().text = SelectedHero.GetComponent<Hero>().Name;
        Panel.transform.GetChild(2).gameObject.GetComponent<Slider>().maxValue = SelectedHero.GetComponent<Hero>().MaxHP;
        Panel.transform.GetChild(2).gameObject.GetComponent<Slider>().value = SelectedHero.GetComponent<Hero>().HP;
        Panel.transform.GetChild(2).gameObject.GetComponent<Slider>().maxValue = SelectedHero.GetComponent<Hero>().MaxHP;
        Panel.transform.GetChild(3).gameObject.GetComponent<Slider>().value = SelectedHero.GetComponent<Hero>().MP;
        Panel.transform.GetChild(5).gameObject.GetComponent<Text>().text = SelectedHero.GetComponent<Hero>().Attack.ToString();
        Panel.transform.GetChild(7).gameObject.GetComponent<Text>().text = SelectedHero.GetComponent<Hero>().Defence.ToString();
    }

    IEnumerator WaitNewRay()
    {
        yield return new WaitForSeconds(0.5f);
        SetRay(SelectedHero);
    }

    private void DeselectTile()
    {
        Panel.SetActive(false);


        if (Tiles.Count > 0)
        {
            foreach (var tile in Tiles)
            {
                tile.GetComponent<Tile>().CanUse = false;
                tile.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                tile.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                tile.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                tile.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            Tiles = new List<GameObject>();
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
                    SetHighlighting(ray);
                    break;
                case 1:
                    Ray ray1 = new Ray(new Vector3(gameObject.transform.position.x - 30, gameObject.transform.position.y + 30, gameObject.transform.position.z), gameObject.transform.up * -30);
                    SetHighlighting(ray1);
                    break;
                case 2:
                    Ray ray2 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 30, gameObject.transform.position.z + 30), gameObject.transform.up * -30);
                    SetHighlighting(ray2);
                    break;
                case 3:
                    Ray ray3 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 30, gameObject.transform.position.z - 30), gameObject.transform.up * -30);
                    //œËÏÂ ‰Â·‡„‡ ‰Îˇ ÎÛ˜‡ 
                    //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 30, transform.position.z - 30), transform.up * -30, Color.red);
                    SetHighlighting(ray3);
                    break;
            }
        }
    }

    private void SetHighlighting(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Tile>() != null)
            {
                if (hit.collider.gameObject.GetComponent<Tile>().CanStep == true)
                {
                    hit.collider.gameObject.GetComponent<Tile>().CanUse = true;
                    hit.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    hit.collider.gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    hit.collider.gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    hit.collider.gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    Tiles.Add(hit.collider.gameObject);
                }
            }
        }
    }

}

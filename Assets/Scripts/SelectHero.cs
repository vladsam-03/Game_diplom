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
    public List<GameObject> RIP;
    public GameObject SelectedPlayer;
    public GameObject SelectedEnemy;
    private bool isPreparation;
    private bool isSelectPlayer;
    private MoveBot moveBot;
    private bool isBarrierPlusX;
    private bool isBarrierMinusX;
    private bool isBarrierPlusZ;
    private bool isBarrierMinusZ;
    public List<Player> Players;

    public Text Name;
    public Slider HP;
    public Text HPCurrent;
    public Slider MP;
    public Text MPCurrent;
    public Text Attack;
    public Text Defense;
    public Slider AttackAP;
    public Text CurrentAttackAP;
    public Slider MoveAP;
    public Text CurrentMoveAP;


    void Start()
    {
        moveBot = GetComponent<MoveBot>();
        MainCamera = Camera.main;
    }

    void Update()
    {
        if (SelectedPlayer != null && isSelectPlayer)
            Stats(SelectedPlayer);
        else if (SelectedEnemy != null && !isSelectPlayer)
            Stats(SelectedEnemy);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Player>() != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && hit.collider.gameObject.GetComponent<StepNPCNew>().step != true)
                {
                    SelectedPlayer = hit.collider.gameObject;
                    MoveCameraToPlayer(SelectedPlayer);
                    isSelectPlayer = true;
                    if (SelectedPlayer.GetComponent<Hero>().MoveAP > 0 && SelectedPlayer.GetComponent<Player>().IsCanMove)
                    {
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
                        SetRayHorizontal(SelectedPlayer);
                        SetRay(SelectedPlayer);
                        Panel.SetActive(true);
                        foreach (var tile in Tiles)
                        {
                            if (tile.GetComponent<Tile>().Region != SelectedPlayer.GetComponent<Player>().SelectTile.Region)
                            {
                                SelectedPlayer.GetComponent<Player>().OtherRegions.Add(tile.GetComponent<Tile>().Region);
                            }
                        }
                    }
                    else if (SelectedPlayer.GetComponent<Hero>().AttackAP > 0 && SelectedPlayer.GetComponent<Player>().IsCanMove)
                    {
                        SetRay(SelectedPlayer);
                    }
                }
                else if (hit.collider.transform.parent.GetComponent<Region>() != null && SelectedPlayer != null 
                    && SelectedPlayer.GetComponent<Player>().OtherRegions.Contains(hit.collider.transform.parent.GetComponent<Region>()) 
                    && !hit.collider.transform.parent.GetComponent<Region>().IsVisited)
                {
                    SelectedPlayer.GetComponent<Player>().MoveAP--;
                    Region region = hit.collider.transform.parent.GetComponent<Region>();
                    region.IsVisited = true;
                    region.Fog.SetActive(false);
                    foreach (var border in region.Borders)
                    {
                        border.SetActive(false);
                    }
                }
                else if (hit.collider.GetComponent<Enemy>() != null && SelectedPlayer != null && SelectedEnemy != null && isPreparation && SelectedPlayer.GetComponent<Hero>().AttackAP > 0
                    && SelectedEnemy == hit.collider.gameObject && Vector3.Distance(SelectedPlayer.transform.position, hit.collider.gameObject.transform.position) == 30
                    && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    hit.collider.GetComponent<Enemy>().HP -= SelectedPlayer.GetComponent<Player>().Attack * (1 - hit.collider.GetComponent<Enemy>().Defence / 100);
                    if (hit.collider.GetComponent<Enemy>().HP > 0)
                    {
                        MeshRenderer[] meshRenderers = hit.collider.gameObject.transform.GetComponentsInChildren<MeshRenderer>();
                        StartCoroutine(AttackColor(meshRenderers));
                    }
                    SelectedPlayer.GetComponent<Hero>().AttackAP--;
                    if (hit.collider.GetComponent<Enemy>().HP <= 0)
                    {
                        Vector3 Rip = SelectedEnemy.transform.position;
                        SelectedEnemy.GetComponent<Enemy>().CurrentTarget.GetComponent<Tile>().IsAttackPlayer = false;
                        SelectedEnemy.GetComponent<Enemy>().CurrentTarget.GetComponent<Tile>().CanStep = true;
                        float RipRotation = SelectedEnemy.transform.rotation.y;
                        moveBot.RemoveBot(SelectedEnemy.GetComponent<Enemy>());
                        Destroy(SelectedEnemy);
                        Panel.SetActive(false);
                        SelectedEnemy = null;

                        int indexRegion = Random.Range(0, RIP.Count);
                        var Regione = RIP[indexRegion];
                        GameObject SelectedRegione = Instantiate(Regione.gameObject, new Vector3(Rip.x, Rip.y, Rip.z), Quaternion.Euler(0, RipRotation, 0));
                    }
                    isPreparation = false;
                }
                else if (hit.collider.GetComponent<Enemy>() != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    SelectedEnemy = hit.collider.gameObject;
                    isSelectPlayer = false;
                    Panel.SetActive(true);

                    MoveCameraToPlayer(SelectedEnemy);
                    if (SelectedPlayer != null)
                    {
                        isPreparation = true;
                    }
                }
                else if (hit.collider.GetComponent<Tile>() != null
                    && hit.collider.GetComponent<Tile>().CanUse == true
                    && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    if (SelectedPlayer.GetComponent<Player>().SelectTile != null)
                    {
                        SelectedPlayer.GetComponent<Player>().SelectTile.CanStep = true;
                        SelectedPlayer.GetComponent<Player>().SelectTile.CurrentPerson = null;
                    }
                    SelectedPlayer.GetComponent<StepNPCNew>().step = true;
                    hit.collider.GetComponent<Tile>().CanStep = false;
                    hit.collider.GetComponent<Tile>().CurrentPerson = SelectedPlayer.GetComponent<Player>();
                    hit.collider.GetComponent<Tile>().IsAttackEnemy = true;
                    SelectedPlayer.GetComponent<Player>().SelectTile = hit.collider.gameObject.GetComponent<Tile>();
                    DeselectTile();
                    StartCoroutine("WaitNewRay");
                    Panel.SetActive(true);
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
        if (SelectedPlayer != null && SelectedPlayer.GetComponent<StepNPCNew>().step)
            MoveCameraToPlayer(SelectedPlayer);
    }

    private void MoveCameraToPlayer(GameObject selectedHero)
    {
        if (selectedHero.transform.rotation.eulerAngles.y == 0) // ??????
        {
            Porstrait.transform.position = new Vector3(selectedHero.transform.position.x, selectedHero.transform.position.y + 25, selectedHero.transform.position.z + 25);
            Porstrait.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (selectedHero.transform.rotation.eulerAngles.y == 90 || selectedHero.transform.rotation.eulerAngles.y == -270) // ??????
        {
            Porstrait.transform.position = new Vector3(selectedHero.transform.position.x + 25, selectedHero.transform.position.y + 25, selectedHero.transform.position.z);
            Porstrait.transform.rotation = Quaternion.Euler(0, 270, 0);

        }
        else if (selectedHero.gameObject.transform.rotation.eulerAngles.y == 180) //?????
        {
            Porstrait.transform.position = new Vector3(selectedHero.transform.position.x, selectedHero.transform.position.y + 25, selectedHero.transform.position.z - 25);
            Porstrait.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (selectedHero.transform.rotation.eulerAngles.y == -90 || selectedHero.transform.rotation.eulerAngles.y == 270) //?????
        {
            Porstrait.transform.position = new Vector3(selectedHero.transform.position.x - 25, selectedHero.transform.position.y + 25, selectedHero.transform.position.z);
            Porstrait.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Stats(GameObject selectedHero)
    {
        Name.text = selectedHero.GetComponent<Hero>().Name;
        HP.value = selectedHero.GetComponent<Hero>().HP / selectedHero.GetComponent<Hero>().MaxHP;
        HPCurrent.text = selectedHero.GetComponent<Hero>().HP + "/" + selectedHero.GetComponent<Hero>().MaxHP; 
        MP.value = selectedHero.GetComponent<Hero>().MP / selectedHero.GetComponent<Hero>().MaxMP;
        MPCurrent.text = selectedHero.GetComponent<Hero>().MP + "/" + selectedHero.GetComponent<Hero>().MaxMP;
        Attack.text = selectedHero.GetComponent<Hero>().Attack.ToString();
        Defense.text = selectedHero.GetComponent<Hero>().Defence.ToString() + "%";
        AttackAP.value = selectedHero.GetComponent<Hero>().AttackAP / selectedHero.GetComponent<Hero>().AttackMaxAP;
        CurrentAttackAP.text = selectedHero.GetComponent<Hero>().AttackAP + "/" + selectedHero.GetComponent<Hero>().AttackMaxAP;
        MoveAP.value = selectedHero.GetComponent<Hero>().MoveAP / selectedHero.GetComponent<Hero>().MoveMaxAP;
        CurrentMoveAP.text = selectedHero.GetComponent<Hero>().MoveAP + "/" +  selectedHero.GetComponent<Hero>().MoveMaxAP;
    }



    IEnumerator WaitNewRay()
    {
        yield return new WaitForSeconds(0.5f);
            SetRayHorizontal(SelectedPlayer);
            SetRay(SelectedPlayer);
    }

    IEnumerator AttackColor(MeshRenderer[] meshRenderers)
    {
        List<Color> colors = new List<Color>();
        foreach (var item in meshRenderers)
        {
            colors.Add(item.material.color);
            item.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < colors.Count; i++)
        {
            meshRenderers[i].material.color = colors[i];
        }

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

    public void SetRayHorizontal(GameObject gameObject)
    {
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    Ray ray = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +5, gameObject.transform.position.z), new Vector3(30, 0, 0));
                    //Debug.DrawRay(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.forward * 30, Color.red);
                    SetHighlightingHorizontal(ray, 0);
                    break;
                case 1:
                    Ray ray1 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), new Vector3(-30, 0, 0));
                    //Debug.DrawRay(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.forward * -30, Color.red);
                    SetHighlightingHorizontal(ray1, 1);
                    break;
                case 2:
                    Ray ray2 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), new Vector3(0, 0, 30));
                    //Debug.DrawRay(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.right * 30, Color.red);
                    SetHighlightingHorizontal(ray2, 2);
                    break;
                case 3:
                    Ray ray3 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), new Vector3(0, 0, -30));
                    //Debug.DrawRay(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.right * -30, Color.red);
                    //?????? ?????? ??? ???? 
                    SetHighlightingHorizontal(ray3, 3);
                    break;
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
                    if (!isBarrierPlusX)
                    {
                        Ray ray = new Ray(new Vector3(gameObject.transform.position.x + 30, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.up * -5);
                        SetHighlighting(ray);
                    }
                    isBarrierPlusX = false;
                        break;
                case 1:
                    if (!isBarrierMinusX)
                    {
                        Ray ray1 = new Ray(new Vector3(gameObject.transform.position.x - 30, gameObject.transform.position.y + 5, gameObject.transform.position.z), gameObject.transform.up * -5);
                        SetHighlighting(ray1);
                    }
                    isBarrierMinusX = false;
                    break;
                case 2:
                    if (!isBarrierPlusZ)
                    {
                        Ray ray2 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z + 30), gameObject.transform.up * -5);
                        SetHighlighting(ray2);
                    }
                    isBarrierPlusZ = false;
                    break;
                case 3:
                    if (!isBarrierMinusZ)
                    {
                        Ray ray3 = new Ray(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z - 30), gameObject.transform.up * -5);
                        //?????? ?????? ??? ???? 
                        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 30, transform.position.z - 30), transform.up * -30, Color.red);
                        SetHighlighting(ray3);
                    }
                    isBarrierMinusZ = false;
                    break;
            }
        }
    }

    private void SetHighlightingHorizontal(Ray ray, int compas)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("barrier"))
            {
                switch (compas)
                {
                    case 0:
                        isBarrierPlusX = true;
                            break;
                    case 1:
                        isBarrierMinusX = true;
                            break;
                    case 2:
                        isBarrierPlusZ = true;
                            break;
                    case 3:
                        isBarrierMinusZ = true;
                            break;

                }
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
                if (hit.collider.gameObject.GetComponent<Tile>().CanStep == true && SelectedPlayer.GetComponent<Hero>().MoveAP > 0)
                {
                    hit.collider.gameObject.GetComponent<Tile>().CanUse = true;
                    hit.collider.gameObject.GetComponent<Tile>().IsAttackEnemy = false;
                    hit.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    hit.collider.gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    hit.collider.gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    hit.collider.gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    Tiles.Add(hit.collider.gameObject);
                }
                else
                {
                    if (hit.collider.gameObject.GetComponent<Tile>().IsAttackPlayer == true)
                    {
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
}

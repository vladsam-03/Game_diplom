using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Transform canvas;
    public Transform old;
    private GameObject player;
    public Item item;

    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        old = transform.parent;
        transform.SetParent(canvas);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == canvas)
        {
        transform.SetParent(old);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        player.BroadcastMessage("remove", this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepNPCNew : MonoBehaviour
{
    [SerializeField] private int offset;

    public bool step;
    public float speed;

    private Hero thisHero;

    private void Awake()
    {
        thisHero = GetComponent<Hero>();
    }

    private void Update()
    {
        if (step)
        {
            transform.position = Vector3.MoveTowards(transform.position, thisHero.SelectTile.transform.position, Time.deltaTime * speed);
            transform.LookAt(thisHero.SelectTile.transform.position);
            if (transform.position == thisHero.SelectTile.transform.position)
            {
                step = false;
                thisHero.MoveAP--;
            }
        }
    }

}

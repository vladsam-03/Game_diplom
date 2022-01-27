using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepNPCNew : MonoBehaviour
{
    [SerializeField] private int offset;

    public bool step;
    public float speed;


    private void Update()
    {
        if (step)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetComponent<Hero>().SelectTile.transform.position, Time.deltaTime * speed);
            transform.LookAt(GetComponent<Hero>().SelectTile.transform.position);
            if (transform.position == GetComponent<Hero>().SelectTile.transform.position)
            {
                step = false;
            }
        }
    }

}

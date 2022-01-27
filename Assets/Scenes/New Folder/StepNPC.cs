using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepNPC : MonoBehaviour
{
    private bool isMoveing;
    private Vector3 correntStep;
    private Vector3 NextStep;
    private Vector3 LastStep;
    public new Rigidbody rigidbody;
    private Vector3 Direction;
    private Button currentBatton;
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    public bool isBot;
    // Start is called before the first frame update
    #region MovePlayer1

    public void StepUp()
    {
        if (rigidbody.position.z == 660)
            return;
        NextStep = new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z + 30);
        rigidbody.velocity = new Vector3(0, 0, 50);
        Direction = new Vector3(0, 0, 1);
        LastStep = rigidbody.position;


        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 30);

    }

    public void StepDown()
    {
        if (gameObject.transform.position.z <= -120 || gameObject.transform.position.z - 30 <= -150)
            return;
        NextStep = new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z - 30);
        rigidbody.velocity = new Vector3(0, 0, -50);
        Direction = new Vector3(0, 0, -1);
        LastStep = rigidbody.position;
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 30);
    }
    public void StepLeft()
    {
        if (gameObject.transform.position.x <= -120 || gameObject.transform.position.z <= -150 && gameObject.transform.position.x <= 240)
            return;
        NextStep = new Vector3(rigidbody.position.x - 30, rigidbody.position.y, rigidbody.position.z);
        rigidbody.velocity = new Vector3(-50, 0, 0);
        Direction = new Vector3(-1, 0, 0);
        LastStep = rigidbody.position;
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x - 30, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    public void StepRight()
    {
        if (gameObject.transform.position.x >= 660 || gameObject.transform.position.z <= -150 && gameObject.transform.position.x >= 300)
            return;
        NextStep = new Vector3(rigidbody.position.x + 30, rigidbody.position.y, rigidbody.position.z);
        rigidbody.velocity = new Vector3(50, 0, 0);
        Direction = new Vector3(1, 0, 0);
        LastStep = rigidbody.position;
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + 30, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Direction.x > 0)
        {
            if (NextStep != new Vector3(0, 0, 0) && rigidbody.position.x >= NextStep.x)
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.position = NextStep;
                correntStep = NextStep;
                NextStep = new Vector3(0, 0, 0);
                if (isBot == true && button4 != null)
                    Move(button4);
                else if (currentBatton != null && isBot == false)
                Move(currentBatton, button1, button2, button3, button4);
            }
        }
        else if (Direction.x < 0)
        {
            if (NextStep != new Vector3(0, 0, 0) && rigidbody.position.x <= NextStep.x)
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.position = NextStep;
                correntStep = NextStep;
                NextStep = new Vector3(0, 0, 0);
                if (isBot == true && button4 != null)
                    Move(button4);
                else if (currentBatton != null && isBot == false)
                    Move(currentBatton, button1, button2, button3, button4);
            }
        }

        else if (Direction.z > 0)
        {
            if (NextStep != new Vector3(0, 0, 0) && rigidbody.position.z >= NextStep.z)
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.position = NextStep;
                correntStep = NextStep;
                NextStep = new Vector3(0, 0, 0);
                if (isBot == true && button4 != null)
                    Move(button4);
                else if (currentBatton != null && isBot == false)
                    Move(currentBatton, button1, button2, button3, button4);
            }
        }
        else if (Direction.z < 0)
        {
            if (NextStep != new Vector3(0, 0, 0) && rigidbody.position.z <= NextStep.z)
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.position = NextStep;
                correntStep = NextStep;
                NextStep = new Vector3(0, 0, 0);
                if (isBot == true && button4 != null)
                    Move(button4);
                else if (currentBatton != null && isBot == false)
                    Move(currentBatton, button1, button2 , button3, button4);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {  
        if (collision.gameObject.CompareTag("barrier"))
        {
            NextStep = new Vector3(0, 0, 0);
            isMoveing = false;
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.position = LastStep;
            if (currentBatton != null)
                currentBatton.interactable = true;
            if (button1 != null)
                button1.interactable = true;
            if (button2 != null)
                button2.interactable = true;
            if (button3 != null)
                button3.interactable = true;
            if (button4 != null)
                button4.interactable = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            NextStep = new Vector3(0, 0, 0);
            rigidbody.velocity = new Vector3(0, 0, 0);
            if (isMoveing)
            {
                rigidbody.position = LastStep;
                isMoveing = false;
            }
            else
                rigidbody.position = correntStep;
            if (currentBatton != null)
                currentBatton.interactable = true;
            if (button1 != null)
                button1.interactable = true;
            if (button2 != null)
                button2.interactable = true;
            if (button3 != null)
                button3.interactable = true;
            if (button4 != null)
                button4.interactable = true;
        }
    }

    public void Move(Button currentButtin, Button Button1, Button Button2, Button Button3, Button Button4)
    {
        currentBatton = currentButtin;
        button1 = Button1;
        button2 = Button2;
        button3 = Button3;
        button4 = Button4;
        currentButtin.interactable = !currentButtin.interactable;
        Button1.interactable = !Button1.interactable;
        Button2.interactable = !Button2.interactable;
        Button3.interactable = !Button3.interactable;
        Button4.interactable = !Button4.interactable;
        isMoveing = !isMoveing;
    }

    public void Move( Button Button4)
    {
        button4 = Button4;
        Button4.interactable = !Button4.interactable;
        isMoveing = !isMoveing;
    }

    private void Start()
    {
        Direction = new Vector3(0, 0, 0);
    }
}

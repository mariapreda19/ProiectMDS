using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight, jump=false, moveLeft, moveRight;
    private string previousKey = "";
    public float speed = 7.0f;
    public float jumpSpeed = 8.0f; // Adjust as needed
    private CharacterController myCharacterController;
    private Animator myAnimator;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        turnLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        turnRight = Input.GetKeyDown(KeyCode.RightArrow);

        jump = Input.GetKeyDown(KeyCode.Space);
        moveLeft = Input.GetKeyDown(KeyCode.A);
        moveRight = Input.GetKeyDown(KeyCode.D);


        // not letting the player turn back
        if (turnLeft && previousKey != "left") {
            transform.Rotate(new Vector3(0f, -90f, 0f));
            previousKey = "left";
        }
        else if (turnRight && previousKey != "right") {
            transform.Rotate(new Vector3(0f, 90f, 0f));
            previousKey = "right";
        }
        else if (jump) {
            myAnimator.Play("Jump");
            // code for jumping
        }
        /*else if (moveLeft)
            transform.position += new Vector3(-1f, 0f, 0f);
        else if (moveRight)
            transform.position += new Vector3(1f, 0f, 0f);*/
        myCharacterController.SimpleMove(new Vector3(0f,0f,0f));
        myCharacterController.Move(transform.forward * speed * Time.deltaTime);
    }
    //trb implemnetat jumpul si modificat distanta dintre tiles.
    
}

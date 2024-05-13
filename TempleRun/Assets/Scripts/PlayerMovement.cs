using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight, roll = false, jump=false, moveLeft, moveRight;
    private string previousKey = "";
    private float horizontalInput;
    private float runningSpeed = 7.0f;
    private float movingSpeed = 10.0f;
    public float jumpSpeed = 8.0f; // Adjust as needed
    private CharacterController myCharacterController;
    private Animator myAnimator;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // not letting the player turn back
        if (turnLeft ) {
            transform.Rotate(new Vector3(0f, -90f, 0f));
            previousKey = "left";
        }
        else if (turnRight ) {
            transform.Rotate(new Vector3(0f, 90f, 0f));
            previousKey = "right";
        }
        else if (jump) {
            myAnimator.Play("Jump");
            // code for jumping
        }
        else if (roll) {
            myAnimator.Play("Roll");
        }
        myCharacterController.SimpleMove(new Vector3(0f,0f,0f));
        //myCharacterController.Move(speed * Time.deltaTime * (transform.forward + movement));

        Vector3 forwardMovement = Time.deltaTime * runningSpeed * transform.forward;
        Vector3 horizontalMovement = Time.deltaTime * movingSpeed * horizontalInput * transform.right;
        myCharacterController.Move(forwardMovement + horizontalMovement);

        // discrete movement
        turnLeft = Input.GetKeyDown(KeyCode.F);
        turnRight = Input.GetKeyDown(KeyCode.H);

        jump = Input.GetKeyDown(KeyCode.Space);
        roll = Input.GetKeyDown(KeyCode.R);
        
        // continuous movement
        horizontalInput = Input.GetAxis("Horizontal");
    }
    //trb implemnetat jumpul si modificat distanta dintre tiles.
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight, roll = false, jump=false, moveLeft, moveRight;
    private string previousKey = "";
    private float horizontalInput;
    private float runningSpeed = 7.0f;
    private float slowdownSpeed = 5.0f;
    private float movingSpeed = 10.0f;
    public float jumpSpeed = 8.0f; // Adjust as needed
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private bool isSlowedDown = false;

    public void setSpeed(float val){
        this.runningSpeed = val;
    }
    public void slowdown(){
        float temp = this.runningSpeed; //keep the previous runningSpeed so we know to what to set it back
        if(!isSlowedDown){ //only slow down if the player is not under this effect already
            setSpeed(slowdownSpeed);
            isSlowedDown = true; //effect has been applied
            Task.Delay(10000).ContinueWith(_ => { //10 s duration for slowdown effect
                setSpeed(temp); //reverse effect
                isSlowedDown = false; //not slowed down anymore
            });
        }
        
    }

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // discrete movement
        turnLeft = Input.GetKeyDown(KeyCode.A);
        turnRight = Input.GetKeyDown(KeyCode.D);

        jump = Input.GetKeyDown(KeyCode.Space);
        roll = Input.GetKeyDown(KeyCode.R);
        
        // continuous movement
        horizontalInput = Input.GetAxis("Horizontal");
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
        else if (roll) {
            myAnimator.Play("Roll");
        }
        myCharacterController.SimpleMove(new Vector3(0f,0f,0f));
        //myCharacterController.Move(speed * Time.deltaTime * (transform.forward + movement));

        Vector3 forwardMovement = Time.deltaTime * runningSpeed * transform.forward;
        Vector3 horizontalMovement = Time.deltaTime * movingSpeed * horizontalInput * transform.right;
        myCharacterController.Move(forwardMovement + horizontalMovement);
        

    }


    
}

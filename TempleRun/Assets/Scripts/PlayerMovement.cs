using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    public bool turnLeft, turnRight, roll = false, jump = false, moveLeft, moveRight;
    private string previousKey = "";
    private float horizontalInput;
    public float runningSpeed = 7.0f; // public for tests
    public float slowdownSpeed = 5.0f;
    public float movingSpeed = 10.0f; // public for tests
    public float jumpSpeed = 10.0f; // Adjust as needed
    private float gravity = 9.81f;
    private CharacterController myCharacterController;
    private Animator myAnimator;
    private bool isSlowedDown = false;
    private float verticalVelocity = 0; // Added to handle vertical movement

    public void setSpeed(float val){
        this.runningSpeed = val;
    }

    public float getSpeed(){
        return runningSpeed;
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

    public void Update() // public for editor tests
    {
        if (Time.timeScale == 0) {
            myCharacterController.SimpleMove(Vector3.zero);

            Vector3 forwardMovement1 = Time.deltaTime * runningSpeed * transform.forward;
            Vector3 horizontalMovement1 = Time.deltaTime * movingSpeed * horizontalInput * transform.right;
            myCharacterController.Move(forwardMovement1 + horizontalMovement1);

            return;
        }

        // Discrete movement
        turnLeft = Input.GetKeyDown(KeyCode.A);
        turnRight = Input.GetKeyDown(KeyCode.D);
        jump = Input.GetKeyDown(KeyCode.Space);
        roll = Input.GetKeyDown(KeyCode.R);
        
        // Continuous movement
        horizontalInput = Input.GetAxis("Horizontal");

        // Not letting the player turn back
        if (turnLeft && previousKey != "left") {
            transform.Rotate(new Vector3(0f, -90f, 0f));
            previousKey = "left";
        }
        else if (turnRight && previousKey != "right") {
            transform.Rotate(new Vector3(0f, 90f, 0f));
            previousKey = "right";
        }

        // Custom ground check to prevent falling between tiles
        bool isGrounded = CheckIfGrounded();

        // Handle jump
        if (isGrounded && jump) {
            verticalVelocity = jumpSpeed;
            myAnimator.Play("Jump");
        } else if (!isGrounded) {
            verticalVelocity -= gravity * Time.deltaTime;
        } else {
            verticalVelocity = 0;
        }

        // Apply vertical movement (jumping and gravity)
        Vector3 verticalMovement = new Vector3(0, verticalVelocity, 0);
        myCharacterController.Move(verticalMovement * Time.deltaTime);

        // Move the player horizontally only
        Vector3 forwardMovement = Time.deltaTime * runningSpeed * transform.forward;
        Vector3 horizontalMovement = Time.deltaTime * movingSpeed * horizontalInput * transform.right;
        myCharacterController.Move(forwardMovement + horizontalMovement);
    }

    private bool CheckIfGrounded() {
        // Perform a raycast slightly below the character to check if grounded
        RaycastHit hit;
        float rayDistance = 0.1f; // Distance for ground check

        if (Physics.Raycast(transform.position, Vector3.down, out hit, myCharacterController.height / 2 + rayDistance)) {
            return true;
        }
        return false;
    }
}

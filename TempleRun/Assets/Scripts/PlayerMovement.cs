using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight, roll = false, jump = false;
    private string previousKey = "";
    private float horizontalInput;
    private float runningSpeed = 7.0f;
    private float slowdownSpeed = 5.0f;
    private float movingSpeed = 10.0f;
    public float jumpSpeed = 10.0f; // Adjust as needed
    public float gravity = 9.81f;
    public float rollSpeed = 8.0f;
    public float rollDuration = 0.5f;
    private bool isRolling = false;
    private float rollTimer;
    private float verticalVelocity = 0;

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
        bool isGrounded = myCharacterController.isGrounded;

        if (turnLeft) {
            transform.Rotate(new Vector3(0f, -90f, 0f));
            previousKey = "left";
        }
        else if (turnRight) {
            transform.Rotate(new Vector3(0f, 90f, 0f));
            previousKey = "right";
        }

        // Handle jump
        if (isGrounded && jump) {
            verticalVelocity = jumpSpeed;
            myAnimator.Play("Jump");
        } else if (!isGrounded) {
            verticalVelocity -= gravity * Time.deltaTime;
        } else {
            verticalVelocity = 0;
        }

        // Handle roll
        if (roll && !isRolling) {
            isRolling = true;
            rollTimer = rollDuration;
            myAnimator.Play("Roll");
        }
        if (isRolling) {
            if (rollTimer > 0) {
                Vector3 rollMovement = transform.forward * rollSpeed * Time.deltaTime;
                myCharacterController.Move(rollMovement);
                rollTimer -= Time.deltaTime;
            } else {
                isRolling = false;
            }
        }

        // Perform movement only if not rolling
        if (!isRolling) {
            Vector3 forwardMovement = Time.deltaTime * runningSpeed * transform.forward;
            Vector3 horizontalMovement = Time.deltaTime * movingSpeed * horizontalInput * transform.right;
            myCharacterController.Move(forwardMovement + horizontalMovement);
        }

        // Update movement states from input
        turnLeft = Input.GetKeyDown(KeyCode.F);
        turnRight = Input.GetKeyDown(KeyCode.H);
        jump = Input.GetKeyDown(KeyCode.Space);
        roll = Input.GetKeyDown(KeyCode.R);
        horizontalInput = Input.GetAxis("Horizontal");

        // Apply vertical movement (jumping and gravity)
        Vector3 verticalMovement = new Vector3(0, verticalVelocity, 0);
        myCharacterController.Move(verticalMovement * Time.deltaTime);
    }
}

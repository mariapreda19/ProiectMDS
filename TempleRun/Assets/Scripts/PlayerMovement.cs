using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight, jump=false;
    public float speed = 7.0f;
    public float jumpSpeed = 8.0f; // Adjust as needed
    private CharacterController myCharacterController;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        turnLeft = Input.GetKeyDown(KeyCode.A);
        turnRight = Input.GetKeyDown(KeyCode.D);
        jump = Input.GetKeyDown(KeyCode.Space);

        if (turnLeft)
            transform.Rotate(new Vector3(0f, -90f, 0f));
        else if (turnRight)
            transform.Rotate(new Vector3(0f, 90f, 0f));
        else if (jump)
            myAnimator.Play("Jump");

        myCharacterController.SimpleMove(new Vector3(0f,0f,0f));
        myCharacterController.Move(transform.forward * speed * Time.deltaTime);
    }
    //trb implemnetat jumpul si modificat distanta dintre tiles.
    
}

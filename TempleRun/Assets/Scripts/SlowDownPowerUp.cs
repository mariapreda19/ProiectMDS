using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlowDownPowerUp : MonoBehaviour
{
    private float turnSpeed = 90f;
    [SerializeField]
    private PlayerMovement movement;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<Player>() != null) //we only want this effect if the other object is the player
            movement.slowdown();
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,turnSpeed*Time.deltaTime);
    }
}

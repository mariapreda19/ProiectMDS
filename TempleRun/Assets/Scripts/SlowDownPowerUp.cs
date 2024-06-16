using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlowDownPowerUp : MonoBehaviour
{
    private float turnSpeed = 90f;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){
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

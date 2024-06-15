using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerUp : MonoBehaviour
{
    public float turnSpeed = 90f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,turnSpeed*Time.deltaTime);
    }
}

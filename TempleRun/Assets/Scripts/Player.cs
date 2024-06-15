using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int money = 0;

    public void updateMoney(int ammount)
    {
        money += ammount;
    }
    public int getMoney()
    {
        return money;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Coin>() != null)
        {
            money++;
            //Debug.Log(money);
            GameManager.instance.UpdateMoney(1);
        }
        else if (other.gameObject.GetComponent<Obstacle>() != null) {
            Debug.Log("hit");
            GameManager.instance.setGameOver(true);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
    }
    void checkDeath(){
        if(transform.position.y <= 1.5f){
            GameManager.instance.setGameOver(true);
        }
        //You can add multiple death causes here...
    }
}

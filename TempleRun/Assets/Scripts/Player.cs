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
            Debug.Log(money);
            GameManager.instance.UpdateMoney(1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

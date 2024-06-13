using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]

    private static int money = 0;
    private static float score = 0;
    private static string playerName = "New Player";

    public static void setName(string val){
        //Debug.Log(val);
        if(val != "")
            playerName = val;
    }

    public static string getName(){
        return playerName;
    }

    public static int getMoney()
    {
        return money;
    }
    public static float getScore()
    {
        return score;
    }

    public static void UpdateMoney(int amount) {
        money += amount;
    }
    public static void UpdateScore(float amount) {
        score += amount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Coin>() != null)
        {
            money++;
            //Debug.Log(money);
            GameManager.instance.UpdateMoney(1);
        }
        if(other.gameObject.GetComponent<ScorePowerUp>() != null)
        {
            GameManager.instance.UpdateScore(200);
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

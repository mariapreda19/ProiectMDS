using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentMoney = 0;
    //private int currentScore = 0;
    //private int highScore = 0;

    public Text moneyText;
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void UpdateMoney(int amount)
    {
        currentMoney += amount;
        moneyText.text = "Money: " + currentMoney;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

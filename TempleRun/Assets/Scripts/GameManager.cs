using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentMoney = 0;
    private int currentScore = 0;
    private int highScore = 0;

    public Text moneyText;
    public Text scoreText;
    // public Text highScoreText;
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
        //UpdateScore(amount * 5);
    }

    public void UpdateScore(int amount)
    {
        currentScore += amount;
        if(currentScore > highScore)
            highScore = currentScore;
        scoreText.text = "Score: " + currentScore.ToString();// + " High Score: " + highScore.ToString();
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateScore((int)(Time.deltaTime * 100));
    }
}

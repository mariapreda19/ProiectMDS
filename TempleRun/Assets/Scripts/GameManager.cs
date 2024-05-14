using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentMoney = 0;
    private float currentScore = 0f;
    private float highScore = 0f;
    private bool gameOver = false;
    public Text moneyText;
    public Text scoreText;
    [SerializeField]
    private GameObject gameOverCanvas;
    // public Text highScoreText;
    public static GameManager instance;

    public void setGameOver(bool val){
        gameOver = val;
    }

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

    public void UpdateScore(float amount)
    {
        currentScore += amount;
        if(currentScore > highScore)
            highScore = currentScore;
        scoreText.text = "Score: " + (int)currentScore;// + " High Score: " + highScore.ToString();
    }

    void Start()
    {
        
    }
    void PauseGame(){
        Time.timeScale = 0;
    }
    void ResumeGame(){
        Time.timeScale = 1;
        gameOver = false;
    }
    void Update()
    {
        if(gameOver){
            PauseGame();
            gameOverCanvas.SetActive(true);
        }
        UpdateScore(10 * Time.deltaTime);
    }
    
    public void Restart(){
        //this should be replaced with main menu scene later
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }
}

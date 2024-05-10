using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


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
    [SerializeField]
    private GameObject mainMenuCanvas;

    public static GameManager instance;
    [SerializeField]
    private TMP_Dropdown DropDownSong;
    [SerializeField]
    private AudioSource Song;
    [SerializeField]
    private AudioClip[] Clips;

    [SerializeField]
    private TMP_Dropdown DropDownSkin;

    public void setSong(){
        string song_name = DropDownSong.options[DropDownSong.value].text;
        if(song_name == "Dan Spataru - Drumurile"){
            Song.clip = Clips[0];
        }
        else if (song_name == "Stereo Love"){
            Song.clip = Clips[2];
        }
        else if (song_name == "Generic - Banii n-aduc fericirea"){
            Song.clip = Clips[1];
        }
        else{
            Song.clip = Clips[0];
        }
    }
    public void setSking(){
        string skin_name = DropDownSkin.options[DropDownSkin.value].text;
        if(skin_name == "PSN"){
        }
        else if(skin_name == "MISSES BLACK"){
            
        }
        else if (skin_name == "THE PEACOCK"){
            
        }
    }


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
        PauseGame();
        Song.clip = Clips[0];
        mainMenuCanvas.SetActive(true);
    }
    void PauseGame(){
        Time.timeScale = 0;
    }
    void ResumeGame(){
        Song.Play();
        Time.timeScale = 1;
        gameOver = false;
    }
    void Update()
    {
        if(gameOver){
            PauseGame();
            gameOverCanvas.SetActive(true);
        }
    }
    void FixedUpdate(){
        UpdateScore(10 * Time.deltaTime);
    }
    public void StartGame(){
        mainMenuCanvas.SetActive(false);
        SceneManager.LoadScene("SampleScene*");
        ResumeGame();
    }
    public void Restart(){
        SceneManager.LoadScene("SampleScene");
        ResumeGame();
    }
}

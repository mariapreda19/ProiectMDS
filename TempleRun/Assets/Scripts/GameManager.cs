using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;


public class GameManager : MonoBehaviour
{
    private int currentMoney = 0;
    private float currentScore = 0f;
    private float highScore = 0f;
    private bool gameOver = false;
    public Text moneyText;
    public Text scoreText;
    public Text playerNameText;
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

    [SerializeField]
    private TMP_Dropdown DropDownDifficulty;

    [SerializeField]
    private float[] runningSpeeds;

    private float runningSpeed = 7;
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private GameObject Character;


    [SerializeField]
    private GameObject[] Skins;

    [SerializeField]
    private TMP_InputField userName;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private SpawnTile spawnTile;
    [SerializeField]
    private Player player;
    
    public void setPlayerName(){
        Player.setName((string)userName.text);
        playerNameText.text = Player.getName();
    }
    public void setSong(){
        Song.clip = Clips[DropDownSong.value];
        Song.volume = 0.1f;
    }
    public void setSkin(){
        Vector3 currentPosition = Character.transform.position;
        Quaternion currentRotation = Character.transform.rotation;
        Transform parentTransform = Character.transform.parent;
        Destroy(Character);
        Character = Instantiate(Skins[DropDownSkin.value], currentPosition, currentRotation, parentTransform);
        virtualCamera.LookAt = Character.GetComponent<Transform>();
        virtualCamera.Follow = Character.GetComponent<Transform>();
        movement = Character.GetComponent<PlayerMovement>();
        player = Character.GetComponent<Player>();
        player.setPlayerMovement(movement);
        spawnTile.changePlayer(Character);

    }

    public void setRunningSpeed(){
        runningSpeed = runningSpeeds[DropDownDifficulty.value];
            movement.setSpeed(runningSpeed);
        if(DropDownDifficulty.value == 0){
            Song.pitch = 1.0f;
        }
        else if (DropDownDifficulty.value == 1){
            Song.pitch = 1.25f;
        }
        else{
            Song.pitch = 1.5f;
        }
        Song.volume = 0.1f;


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
        moneyText.text = "Money: " + Player.getMoney().ToString();
        //UpdateScore(amount * 5);
    }

    public void UpdateScore(float amount)
    {
        currentScore += amount;
        Player.UpdateScore(amount);
        if(currentScore > highScore)
            highScore = currentScore;
        //scoreText.text = "Score: " + (int)currentScore;// + " High Score: " + highScore.ToString();
        scoreText.text = "Score: " + (int)Player.getScore();
    }

    public void BuyLife()
    {
        if(Player.getMoney() >= 3)
        {
            UpdateMoney(-3);
            Player.UpdateMoney(-3);
            gameOverCanvas.SetActive(false);
            SceneManager.LoadScene("SampleScene");
            ResumeGame();
        }
    }

    void Start()
    {
        PauseGame();
        Song.clip = Clips[0];
        Song.volume = 0.1f;
        mainMenuCanvas.SetActive(true);
        UpdateMoney(0);
        playerNameText.text = Player.getName();
        //playerMesh = Skins[1];

        //Player.UpdateScore(-Player.getScore());
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
        ResumeGame();
        //Player.UpdateScore(-Player.getScore());
    }
    public void Restart(){
        SceneManager.LoadScene("SampleScene");
        ResumeGame();
        Player.UpdateScore(-Player.getScore());
    }
}

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
    // atributele clasei
    private int currentMoney = 0;
    private float currentScore = 0f;
    private static float highScore = 0f;
    private bool gameOver = false;
    public Text moneyText;
    public Text scoreText;
    public Text playerNameText;
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject mainMenuCanvas;

    public static GameManager instance; // instanta unica a GameManager-ului
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
        float currentCharacterRunningSpeed = movement.getSpeed();
        Destroy(Character);
        
        // Crearea unui nou jucator cu skin-ul selectat
        Character = Instantiate(Skins[DropDownSkin.value], currentPosition, currentRotation, parentTransform);
        
        // Setarea camerei virtuale si a miscării jucătorului
        virtualCamera.LookAt = Character.GetComponent<Transform>();
        virtualCamera.Follow = Character.GetComponent<Transform>();
        
        // Setarea vitezei de rulare si a jucstorului
        movement = Character.GetComponent<PlayerMovement>();
        movement.setSpeed(currentCharacterRunningSpeed);
        player = Character.GetComponent<Player>();
        player.setPlayerMovement(movement);
        spawnTile.changePlayer(Character);
    }


    public void setRunningSpeed(){
        // Setează viteza de rulare în funcție de dificultate.
        // De asemenea, setează și pitch-ul melodiei.
        // Acest bloc a fost scris de copilot la scrierea numelui funcției si a 
        // primelor doua comentarii
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
        // se creaza o singura instanta a GameManager-ului
        if(instance == null)
            instance = this;
    }

    public void UpdateMoney(int amount)
    {
        currentMoney += amount;
        moneyText.text = "Money: " + Player.getMoney().ToString();
    }


    public void UpdateScore(float amount)
    {
        // actualizam scorul, iar, daca e cel mai mare, il setam ca high score
        currentScore += amount;
        Player.UpdateScore(amount);
        if(currentScore > highScore)
            highScore = currentScore;
        scoreText.text = "Score: " + (int)currentScore + "\nHigh Score: " + (int)highScore;
    }

    
    public void BuyLife()
    {
        // cumpararea unei vieti daca avem suficienti bani
        if(Player.getMoney() >= 3) 
        {
            // scadem bani si adaugam viata
            UpdateMoney(-3);
            Player.UpdateMoney(-3);
            gameOverCanvas.SetActive(false);
            // revenim la joc
            SceneManager.LoadScene("SampleScene");
            ResumeGame();
        }
    }

    void Start()
    {
        PauseGame();
        Song.clip = Clips[0];
        Song.volume = 0.1f;
        // meniul principal
        mainMenuCanvas.SetActive(true);
        UpdateMoney(0);
        playerNameText.text = Player.getName();
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
            // verificam daca jocul s-a terminat
            PauseGame();
            gameOverCanvas.SetActive(true);
        }
    }


    void FixedUpdate(){
        // actualizarea scorului în funcție de timp
        UpdateScore(10 * Time.deltaTime);
    }

    
    public void StartGame(){
        // Incepem jocul și ascundem meniul
        mainMenuCanvas.SetActive(false);
        ResumeGame();
    }

    
    public void Restart(){
        // Repornim jocul si reseteam scorul
        SceneManager.LoadScene("SampleScene");
        ResumeGame();
        Player.UpdateScore(-Player.getScore());
    }
}

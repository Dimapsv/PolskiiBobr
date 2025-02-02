using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //brevno
    
    public int brevnoIsDelivered;
    public TextMeshProUGUI brevnoCountText;
    public bool brevnoIsTaked;
    

    //lesopilka
    public GameObject branchOfLesopilka;

    // visable upgrade
    public GameObject visable;
    public bool isVisableUpgraded = false;

    //Maze
    public bool isMazed = false;
    public Transform[] chestSpawnPoints;
    public Transform[] keySpawnPoints;
    
    private Transform keySpawnPoint;
    public GameObject chest;
    public GameObject keyLesopilka;
    

    // PauseMenu
    public GameObject pauseMenuPanel;
    public bool isPaused;
    

    
    

    private void Awake()
    {
        brevnoIsDelivered = 0;
        pauseMenuPanel.SetActive(false);
        
        
        
        //SpawnChest();
    }

    private void OnEnable()
    {
        PlayerManager.OnGameOver.AddListener(GameOver);

        PlayerManager.OnKeyOfLesopilkaHasChanged.AddListener(OpenLesopilka);
    }

    private void OnDisable()
    {
        PlayerManager.OnGameOver.RemoveListener(GameOver);

        PlayerManager.OnKeyOfLesopilkaHasChanged.RemoveListener(OpenLesopilka);
    }

    

    //Brevno
    

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        
    }

    public void TextBrevnoUpdate()
    {
        brevnoCountText.text = "" + brevnoIsDelivered + "/10";
    }

    public void TogglePauseMenu()
    {
        if (pauseMenuPanel.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pauses the game
        pauseMenuPanel.SetActive(true);
        isPaused = true;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resumes the game
        pauseMenuPanel.SetActive(false);
        isPaused = false;

    }

    public void GameOver()
    {
        SceneManager.LoadScene("Main Scene");
    }

       

    public void RestartButton()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void MainMenuButton()
    {
        ResumeGame();
        SceneManager.LoadScene(1);
    }

    
    public void OpenLesopilka(bool isOpened)
    {
        if (isOpened)
            branchOfLesopilka.SetActive(false);
        else
            branchOfLesopilka.SetActive(true);
    }


}

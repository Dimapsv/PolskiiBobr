using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Transform[] brevnoSpawnPoints;
    public int brevnoIsDelivered;
    public TextMeshProUGUI brevnoCountText;
    public GameObject brevno;
    private Transform spawnPointBrevno;
    public bool brevnoIsTaked;
    public GameObject isTakedBrevnoIndicator;

    // visable upgrade
    public GameObject visable;
    public bool isVisableUpgraded = false;

    //Maze
    public bool isMazed = false;
    public Transform[] chestSpawnPoints;
    public Transform[] keySpawnPoints;
    private Transform chestSpawnPoint;
    private Transform keySpawnPoint;
    public GameObject chest;
    public GameObject keyPolandDorf;
    

    // PauseMenu
    public GameObject pauseMenuPanel;
    public bool isPaused;
    public GameObject helpMenuPanel;

    //Keys
    public bool isKeyPolandDorf;
    public bool isKeyLesopilka;

    private void Awake()
    {
        brevnoIsDelivered = 0;
        SpawnBrevno();
        pauseMenuPanel.SetActive(false);
        helpMenuPanel.SetActive(false);
        isTakedBrevnoIndicator.SetActive(false);
        SpawnKeyPolandDorf();
        //SpawnChest();
    }

    //Maze
    public void SpawnKeyPolandDorf()
    {
        keySpawnPoint = keySpawnPoints[Random.Range(0, keySpawnPoints.Length)];
        Instantiate(keyPolandDorf, keySpawnPoint);
    }

    public void SpawnChest()
    {
        chestSpawnPoint = chestSpawnPoints[Random.Range(0, chestSpawnPoints.Length)];
        Instantiate(chest, chestSpawnPoint);
    }

    public void MazeEnemyDelete()
    {
        
    }

    //Brevno
    public void SpawnBrevno()
    {
        spawnPointBrevno = brevnoSpawnPoints[Random.Range(0, brevnoSpawnPoints.Length)];
        Instantiate(brevno, spawnPointBrevno);
        if (isVisableUpgraded == true)
        {
            Instantiate(visable, spawnPointBrevno);
        }
        brevnoIsTaked = false;
        TextBrevnoUpdate();

    }

    public void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (brevnoIsTaked == true)
        {
            isTakedBrevnoIndicator.SetActive(true);
        }
        else
        {
            isTakedBrevnoIndicator.SetActive(false);
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


    public void ResumeButton()
    {
        ResumeGame();
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

    public void HelpButton()
    {
        helpMenuPanel.SetActive(true);
    }

    public void BackToPauseMenuButton()
    {
        helpMenuPanel.SetActive(false);
    }


}

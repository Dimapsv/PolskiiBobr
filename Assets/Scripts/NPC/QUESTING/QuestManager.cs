using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    public List<Quest> currentQuests = new List<Quest>();

    public bool brevnoQuestCleared;
    public bool ballForBoyQuestCleared;
    public bool bobrStonesQuestCleared;

    public List<bool> listOfBobrStone = new List<bool>() { false, false, false, false, false };

    public GameObject SpawnerHealthForBall;
    public GameObject SpawnerPushUpgrade;

    private void OnEnable()
    {
        PlayerManager.OnBrevnoValueChanged.AddListener(CheckBrevnoCount);
        PlayerManager.OnBallHasChanged.AddListener(CheckBoyBallHaving);
        PlayerManager.OnBobrStoneTaked.AddListener(CheckBobrStoneHaving);
    }

    private void OnDisable()
    {
        PlayerManager.OnBrevnoValueChanged.RemoveListener(CheckBrevnoCount);
        PlayerManager.OnBallHasChanged.RemoveListener(CheckBoyBallHaving);
        PlayerManager.OnBobrStoneTaked.AddListener(CheckBobrStoneHaving);
    }


    private void Awake() //SingleTone
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        brevnoQuestCleared = false;
        ballForBoyQuestCleared = false;
        bobrStonesQuestCleared = false;

    }

    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
    }

    public bool CheckQuest(int idOfQuest)
    {
        switch (idOfQuest)
        {
            case 0:
                if (brevnoQuestCleared)
                    return true;
                else
                    return false;
            case 1:
                if (ballForBoyQuestCleared)
                    return true;
                else
                    return false;
            case 2:
                if (bobrStonesQuestCleared)
                    return true;
                else
                    return false;


            default: return false;

        }   
    }

    public void SpawnReward(int questId)
    {
        switch (questId)
        {
            case 1:
                if (ballForBoyQuestCleared)
                {
                    SpawnerHealthForBall.SetActive(true);
                }
                break;
            case 2:
                if (bobrStonesQuestCleared)
                {
                    SpawnerHealthForBall.SetActive(true);
                }
                break;
            default: return;
        }
    }



    public void CheckBrevnoCount(int currentBrevnoCount)
    {
             if (currentBrevnoCount >= 5)
            {
                brevnoQuestCleared = true;
                
            }
            else
            {
                brevnoQuestCleared = false;
                Debug.Log(brevnoQuestCleared);
            }

    }

    public void CheckBoyBallHaving(bool ballBoyHas) // ball
    {
        if (ballBoyHas)
            ballForBoyQuestCleared = true;
        else
            ballForBoyQuestCleared = false;
    }


    public void CheckBobrStoneHaving(int idOfBobrStone)
    {
        listOfBobrStone[idOfBobrStone] = true;
        int boolCheckerStones = 0;
        for (int i = 0; i < listOfBobrStone.Count; i++)
        {
            if (listOfBobrStone[i] == true)
            {
                boolCheckerStones++;
            }
        }

        if (boolCheckerStones == 5)
        {
            bobrStonesQuestCleared = true;
        }


    }



}

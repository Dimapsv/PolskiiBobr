using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static UnityEvent<int> OnQuestTaked = new UnityEvent<int>();
    public static UnityEvent<int> OnQuestCleared = new UnityEvent<int>();

    public static QuestManager instance;
    
    public List<Quest> currentQuests = new List<Quest>();

    //quests
    public bool brevnoQuestCleared;
    public bool ballForBoyQuestCleared;
    public bool bobrStonesQuestCleared;
    public bool chickenFermerQuestCleared;
    public bool lesopilkaQuestCleared;
    public bool mommyQuestCleared;
    public bool motoQuestCleared;
    public bool majorQuestCleared;

    public List<bool> listOfBobrStone = new List<bool>() { false, false, false, false, false };

    [SerializeField]
    private int chickenCount;

    [SerializeField]
    private int lesopilkaTreeCount;

    public GameObject SpawnerHealthForBall;
    public GameObject SpawnerPushUpgrade;
    public GameObject SpawnerFuel;
    public GameObject SpawnerLesopilkaTree;
    public GameObject SpawnerMajorTree;

    private void OnEnable()
    {
        ThirdPersonController.OnLesopilkaTreeTaked.AddListener(CheckLesopilkaTreeCount);
        Mommy.OnForbiddenBoyHasChanged.AddListener(CheckMommyQuest);
        GarageSergei.OnMotoHasChanged.AddListener(CheckMotoQuest);

        PlayerManager.OnBrevnoValueChanged.AddListener(CheckBrevnoCount);
        PlayerManager.OnBallHasChanged.AddListener(CheckBoyBallHaving);
        PlayerManager.OnBobrStoneTaked.AddListener(CheckBobrStoneHaving);
        PlayerManager.OnChickenTaked.AddListener(CheckChickenHaving);
    }

    private void OnDisable()
    {
        ThirdPersonController.OnLesopilkaTreeTaked.AddListener(CheckLesopilkaTreeCount);
        Mommy.OnForbiddenBoyHasChanged.RemoveListener(CheckMommyQuest);
        GarageSergei.OnMotoHasChanged.RemoveListener(CheckMotoQuest);

        PlayerManager.OnBrevnoValueChanged.RemoveListener(CheckBrevnoCount);
        PlayerManager.OnBallHasChanged.RemoveListener(CheckBoyBallHaving);
        PlayerManager.OnBobrStoneTaked.RemoveListener(CheckBobrStoneHaving);
        PlayerManager.OnChickenTaked.RemoveListener(CheckChickenHaving);
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
        chickenFermerQuestCleared = false;
        chickenCount = 0;

    }

    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
        OnQuestTaked?.Invoke(quest.id);
    }

    public void ClearQuest(int idOfQuest)
    { 
        OnQuestCleared?.Invoke(idOfQuest);
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
            case 3:
                if (chickenFermerQuestCleared)
                    return true;
                else
                    return false;
            case 4:
                if (lesopilkaQuestCleared)
                    return true;
                else
                    return false;
            case 5:
                if (mommyQuestCleared)
                    return true;
                else
                    return false;
            case 6:
                if (motoQuestCleared)
                    return true;
                else
                    return false;
            case 7:
                if (majorQuestCleared)
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
                    SpawnerPushUpgrade.SetActive(true);
                }
                break;
            case 3:
                if (chickenFermerQuestCleared)
                {
                    SpawnerFuel.SetActive(true);
                }
                break;
            case 4:
                if (lesopilkaQuestCleared)
                {
                    SpawnerLesopilkaTree.SetActive(true);
                }
                break;
            case 7:
                if (majorQuestCleared)
                {
                    SpawnerMajorTree.SetActive(true);
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

        CheckMajorQuest();
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

    public void CheckChickenHaving()
    {
        chickenCount++;

        if (chickenCount >= 4)
        {
            chickenFermerQuestCleared = true;
        }
        else
        {
            chickenFermerQuestCleared = false;
        }
    }

    public void CheckLesopilkaTreeCount()
    {
        lesopilkaTreeCount++;

        if (lesopilkaTreeCount >= 20)
        {
            lesopilkaQuestCleared = true;
        }
        else 
        {
            lesopilkaQuestCleared = false;
        }

    }

    public void CheckMommyQuest()
    {
        mommyQuestCleared = true;
        CheckMajorQuest();
    }

    public void CheckMotoQuest()
    {
        motoQuestCleared = true;
        CheckMajorQuest();
    }

    public void CheckMajorQuest()
    {
        if (motoQuestCleared && mommyQuestCleared && ballForBoyQuestCleared)
        {
            majorQuestCleared = true;
        }
        else
        {
            majorQuestCleared = false;
        }
    }

}

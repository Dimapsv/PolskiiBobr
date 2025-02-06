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

    private void OnEnable()
    {
        PlayerManager.OnBrevnoValueChanged.AddListener(CheckBrevnoCount);
    }

    private void OnDisable()
    {
        PlayerManager.OnBrevnoValueChanged.RemoveListener(CheckBrevnoCount);
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
                

            default: return false;

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


    


}

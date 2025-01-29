using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public bool isBrevnoQuestTaked;
    public bool isBrevnoQuestCompleted;

    public List<Quest> currentQuests = new List<Quest>();

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
        isBrevnoQuestTaked = false;
        isBrevnoQuestCompleted = false;

    }

    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
    }



}

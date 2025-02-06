using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    
    public bool isfirstTalked;
    public DialogueAsset firstDialogue;
    public DialogueAsset postDialogue;
    public DialogueAsset questClearedDialogue;
    public DialogueAsset questNotClearedQuest;
    public Quest quest;
    public int id;

    private void Start()
    {
        if (quest != null)
            id = quest.id;
        
            
    }


}

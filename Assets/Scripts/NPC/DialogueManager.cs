using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public static UnityEvent OnDialogueStart = new UnityEvent();
    public static UnityEvent OnDialogueStop = new UnityEvent();
    private bool skipLineTriggered;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    

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

    public void StartDialogue(DialogueAsset dialogue)
    {
        
        nameText.text = dialogue.nameOfNps;
        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue));
        
    }


    IEnumerator RunDialogue(DialogueAsset dialogue)
    {
        skipLineTriggered = false;
        OnDialogueStart?.Invoke();

        for (int i = 0; i < dialogue.dialogue.Length; i++)
        {
            dialogueText.text = dialogue.dialogue[i];

            while (skipLineTriggered == false)
            {
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueStop?.Invoke();
        dialoguePanel.SetActive(false);


    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
    
}

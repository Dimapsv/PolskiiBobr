using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    private void OnEnable()
    {
        NPC.OnDialogue.AddListener(StartDialogue);
    }

    private void OnDisable()
    {
        NPC.OnDialogue.RemoveListener(StartDialogue);
    }

    public void StartDialogue(DialogueAsset dialogue)
    {
        int numberOfLines = dialogue.dialogue.Length;
        int currentLine = 0;
        dialogueText.text = dialogue.dialogue[currentLine];
        nameText.text = dialogue.nameOfNps;
        dialoguePanel.SetActive(true);
        
    }

    
}

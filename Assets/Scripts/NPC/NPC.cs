using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public static UnityEvent<DialogueAsset> OnDialogue = new UnityEvent<DialogueAsset>();

    [SerializeField] private DialogueAsset dialogue;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            
            OnDialogue.Invoke(dialogue);
        }
    }

    
}

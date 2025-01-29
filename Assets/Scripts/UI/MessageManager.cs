using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MessageManager : MonoBehaviour
{
    public GameObject messagePanel;
    [SerializeField] private TMP_Text messageForPlayerText;

    private void Start()
    {
        messagePanel.SetActive(false);
        messageForPlayerText.text = "";
    }

    private void OnEnable()
    {
        UIManager.OnWriteMessage.AddListener(WriteMessage);
    }

    private void OnDisable()
    {
        UIManager.OnWriteMessage.RemoveListener(WriteMessage);
    }

    private void WriteMessage(int id)
    {
        switch (id)
        {
            case 0:
                messageForPlayerText.text = "Открыта лесопилка!";
                break;
            default:
                messageForPlayerText.text = "Как у вас дела?)";
                break;
        }

        messagePanel.SetActive(true);

        Invoke("HideMessagePanel", 5.0f);
    }


    private void HideMessagePanel()
    {
        messagePanel.SetActive(false);
    }
}

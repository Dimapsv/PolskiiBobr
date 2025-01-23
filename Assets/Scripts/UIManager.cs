using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHealthText;
    public GameObject bloodOverlay; // Try to use DoTweeen for few second looking this


    private void OnEnable()
    {
        PlayerManager.OnHealthValueChanged.AddListener(UIHealthUpdate);
    }

    private void OnDisable()
    {
        PlayerManager.OnHealthValueChanged.RemoveListener(UIHealthUpdate);
    }
        
    private void UIHealthUpdate(int healthValue)
    {
        playerHealthText.text = healthValue.ToString();
    }
}

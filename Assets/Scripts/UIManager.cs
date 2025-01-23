using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHealthText;
    public Image bloodOverlay; // Try to use DoTweeen for few second looking this


    private void Start()
    {
        bloodOverlay.enabled = false;

    }

    private void OnEnable()
    {
        PlayerManager.OnHealthValueChanged.AddListener(UIHealthUpdate);
        PlayerManager.OnDamageTaked.AddListener(UIDamageTaking);
        
    }

    private void OnDisable()
    {
        PlayerManager.OnHealthValueChanged.RemoveListener(UIHealthUpdate);
    }
        
    public void UIHealthUpdate(int healthValue)
    {
     
        playerHealthText.text = healthValue.ToString();
    }

    public void UIDamageTaking()
    {
        ShowBloodImageForSeconds(2f);
    }


    void ShowBloodImageForSeconds(float seconds)
    {
        // Показать изображение
        bloodOverlay.enabled = true;

        // Скрыть изображение через заданное время
        Invoke("HideBloodImage", seconds);
    }

    void HideBloodImage()
    {
        // Скрыть изображение
        bloodOverlay.enabled = false;
    }
}

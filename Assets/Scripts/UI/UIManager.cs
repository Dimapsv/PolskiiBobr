using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;



public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text brevnoCountText;

    public static UnityEvent<int> OnWriteMessage = new UnityEvent<int>();
   
    public Image bloodOverlay; // Try to use DoTweeen for few second looking this

    //inventory
    public Image keyOfLesopilkaImage;

    private void Start()
    {
        bloodOverlay.enabled = false;
        keyOfLesopilkaImage.enabled = false;

    }

    private void OnEnable()
    {
        PlayerManager.OnHealthValueChanged.AddListener(UIHealthUpdate);
        PlayerManager.OnDamageTaked.AddListener(UIDamageTaking);

        PlayerManager.OnBrevnoValueChanged.AddListener(UIBrevnoUpdate);

        PlayerManager.OnKeyOfLesopilkaHasChanged.AddListener(UIKeyOfLesopilkaUpdate);

    }

    private void OnDisable()
    {
        PlayerManager.OnHealthValueChanged.RemoveListener(UIHealthUpdate);
        PlayerManager.OnDamageTaked.RemoveListener(UIDamageTaking);

        PlayerManager.OnBrevnoValueChanged.RemoveListener(UIBrevnoUpdate);

        PlayerManager.OnKeyOfLesopilkaHasChanged.RemoveListener(UIKeyOfLesopilkaUpdate);
        
    }
        
    public void UIHealthUpdate(int healthValue)
    {
        playerHealthText.text = healthValue.ToString();
    }
        
    public void UIDamageTaking()
    {
        ShowBloodImageForSeconds(2f);
    }

    public void UIBrevnoUpdate(int brevnoValue)
    {
        brevnoCountText.text = brevnoValue.ToString() + " / 5";
    }

    public void UIKeyOfLesopilkaUpdate(bool isHasKey)
    {
        keyOfLesopilkaImage.enabled = isHasKey;
        if (isHasKey)
            OnWriteMessage.Invoke(0); // id = 0 - lesopilka opened
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

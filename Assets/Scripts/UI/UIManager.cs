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
    public int idOfMessageL;

    public Image bloodOverlay; // Try to use DoTweeen for few second looking this

    //inventory
    public GameObject inventoryPanel;
    public Image keyOfLesopilkaImage;

    //notes
    public GameObject notesPanel;
    public Image[] notes;

    private void Start()
    {
        bloodOverlay.enabled = false;
        

    }

    private void OnEnable()
    {
        PlayerManager.OnHealthValueChanged.AddListener(UIHealthUpdate);
        PlayerManager.OnDamageTaked.AddListener(UIDamageTaking);

        PlayerManager.OnBrevnoValueChanged.AddListener(UIBrevnoUpdate);

        PlayerManager.OnKeyOfLesopilkaHasChanged.AddListener(UIKeyOfLesopilkaUpdate);

        PlayerManager.OnNoteTaked.AddListener(UINoteUpdate);

    }

    private void OnDisable()
    {
        PlayerManager.OnHealthValueChanged.RemoveListener(UIHealthUpdate);
        PlayerManager.OnDamageTaked.RemoveListener(UIDamageTaking);

        PlayerManager.OnBrevnoValueChanged.RemoveListener(UIBrevnoUpdate);

        PlayerManager.OnKeyOfLesopilkaHasChanged.RemoveListener(UIKeyOfLesopilkaUpdate);

        PlayerManager.OnNoteTaked.RemoveListener(UINoteUpdate);

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
        
        if (isHasKey)
        {
            SpawnChild(inventoryPanel.transform, keyOfLesopilkaImage);
            idOfMessageL = 0;
            ShowMessage(); // id = 0 - lesopilka opened
        }
            
    }

    public void UINoteUpdate(int idOfNote)
    {
        SpawnChild(notesPanel.transform, notes[idOfNote - 1]);
        idOfMessageL = 1;
        ShowMessage(); // id = 1 - new Note!
        idOfMessageL = 2;
        Invoke("ShowMessage", 7.0f);

    }

    public void ShowMessage()
    {
        
        OnWriteMessage.Invoke(idOfMessageL);
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

    public void SpawnChild(Transform parent, Image childObjectPrefab)
    {
        if (childObjectPrefab != null && parent != null)
        {
            GameObject spawnedChild = Instantiate(childObjectPrefab.gameObject, parent.position, parent.rotation);
            spawnedChild.transform.SetParent(parent);
        }
    }
}

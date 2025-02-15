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

    public GameObject bloodOverlay; // Try to use DoTweeen for few second looking this
    public TMP_Text damageText;

    //inventory
    public GameObject inventoryPanel;
    public Image keyOfLesopilkaImage;

    //notes
    public GameObject notesPanel;
    public TMP_Text noteText;

    [TextArea]
    public List<string> noteSentences = new List<string>();

    public List<GameObject> noteButtons = new List<GameObject>();
    
    public List<GameObject> questsInfo = new List<GameObject>();

    public List<GameObject> inventoryImages = new List<GameObject>();
    public List<GameObject> inventoryEmptyImages = new List<GameObject>();

    //fastTrevell
    public GameObject fastTrevelPanel;



    private void Start()
    {
        
        fastTrevelPanel.SetActive(false);
        HideDamageOverLay();


    }

    private void OnEnable()
    {
        PlayerManager.OnCollectablesTaked.AddListener(UIInventoryImagesUpdate);

        QuestManager.OnQuestTaked.AddListener(UIQuestAdd);
        FastTrevel.OnFastTrevelStaying.AddListener(UIFastTrevel);

        PlayerManager.OnHealthValueChanged.AddListener(UIHealthUpdate);
        

        PlayerManager.OnBrevnoValueChanged.AddListener(UIBrevnoUpdate);


        DamageScript.OnDealingDamage.AddListener(ShowDamageOverlay);

        PlayerManager.OnNoteTaked.AddListener(UINoteUpdate);

    }

    private void OnDisable()
    {
        PlayerManager.OnCollectablesTaked.RemoveListener(UIInventoryImagesUpdate);

        QuestManager.OnQuestTaked.RemoveListener(UIQuestAdd);

        PlayerManager.OnHealthValueChanged.RemoveListener(UIHealthUpdate);

        DamageScript.OnDealingDamage.RemoveListener(ShowDamageOverlay);

        PlayerManager.OnBrevnoValueChanged.RemoveListener(UIBrevnoUpdate);


        PlayerManager.OnNoteTaked.RemoveListener(UINoteUpdate);

        FastTrevel.OnFastTrevelStaying.RemoveListener(UIFastTrevel);

    }

    public void UIInventoryImagesUpdate(int idOfImageInventory)
    {

        inventoryEmptyImages[idOfImageInventory].SetActive(false);
        inventoryImages[idOfImageInventory].SetActive(true);

        if (idOfImageInventory == 0)
        {
            idOfMessageL = 0;
            ShowMessage(); // id = 0 - lesopilka opened
        }

    }

    public void UIQuestAdd(int idOfQuest)
    {
        questsInfo[idOfQuest].SetActive(true);
    }

    public void UIHealthUpdate(int healthValue)
    {
        playerHealthText.text = healthValue.ToString();
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
        }
            
    }

    public void UINoteUpdate(int idOfNote)
    {
        noteButtons[idOfNote].SetActive(true);

        idOfMessageL = 1;
        ShowMessage(); // id = 1 - new Note!
        idOfMessageL = 2;
        Invoke("ShowMessage", 7.0f);

    }

    public void ShowNote(int idOfNote)
    {
        notesPanel.SetActive(true);

        noteText.text = noteSentences[idOfNote];
    }

    public void ShowMessage()
    {
        
        OnWriteMessage.Invoke(idOfMessageL);
    }


    

    public void UIFastTrevel(bool isStaying)
    {
        if (isStaying)
        {
            fastTrevelPanel.SetActive(true);
        }
        else
        {
            fastTrevelPanel.SetActive(false);
        }
    }

    public void SpawnChild(Transform parent, Image childObjectPrefab)
    {
        if (childObjectPrefab != null && parent != null)
        {
            
            GameObject spawnedChild = Instantiate(childObjectPrefab.gameObject, parent.position, parent.rotation);
            spawnedChild.transform.SetParent(parent);
            spawnedChild.GetComponent<RectTransform>().localScale = new Vector3(1, 0.5f, 1);
            
            
            
            
        }
    }


    private void ShowDamageOverlay(int damage)
    {
        damageText.text = "-" + damage.ToString();
        damageText.enabled = true;
        bloodOverlay.SetActive(true);
        Invoke("HideDamageOverLay",1.0f);
    }

    private void HideDamageOverLay()
    {
        bloodOverlay.SetActive(false);
        damageText.enabled = false;
    }
}

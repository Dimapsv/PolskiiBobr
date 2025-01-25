using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class PlayerManager : MonoBehaviour
{
    
    public static UnityEvent<int> OnHealthValueChanged = new UnityEvent<int>();
    public static UnityEvent OnDamageTaked = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();


    public int playerHealth;
       

    private void OnEnable()
    {
        DamageScript.OnDealingDamage.AddListener(TakeDamage);
        Item.OnItemTaked.AddListener(GetItem);
    }

    private void OnDisable()
    {
        DamageScript.OnDealingDamage.RemoveListener(TakeDamage);
        Item.OnItemTaked.RemoveListener(GetItem);
    }

    void Start()
    {
        playerHealth = 5;
        OnHealthValueChanged.Invoke(playerHealth);
        
    }

    void Update()
    {
        

        
    }

    public void GetItem(InteractableItemAsset item)
    {
        string itemType = item.itemType.ToString();
        switch (itemType)
        {

            case "Tree":
                Debug.Log("It's Tree");
                break;

            case "Health":
                Debug.Log("Health");
                TakeHealth(item.valueOfHealth);
                break;
        }
            
    }

    private void TakeDamage(int healtValueChange)
    {
        playerHealth -= healtValueChange;

        OnHealthValueChanged.Invoke(playerHealth);
        OnDamageTaked.Invoke();

        if (playerHealth <= 0)
        {
            OnGameOver.Invoke();
        }
    }

    private void TakeHealth(int healthValueChange)
    {
        playerHealth += healthValueChange;
        OnHealthValueChanged.Invoke(playerHealth);
    }


    
}

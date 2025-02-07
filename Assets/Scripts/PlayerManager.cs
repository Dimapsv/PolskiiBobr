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
    
    public static UnityEvent<int> OnBrevnoValueChanged = new UnityEvent<int>();
    public static UnityEvent<bool> OnKeyOfLesopilkaHasChanged = new UnityEvent<bool>();
    public static UnityEvent<bool> OnBallHasChanged = new UnityEvent<bool>();
    public static UnityEvent<int> OnBobrStoneTaked = new UnityEvent<int>();

    public static UnityEvent<int> OnNoteTaked = new UnityEvent<int>();

    public static UnityEvent<int> OnAbilityTaked = new UnityEvent<int>();

    public static UnityEvent OnChickenTaked = new UnityEvent();

    public static UnityEvent<bool> OnFuelTankHasChanged = new UnityEvent<bool>();

    public static UnityEvent<bool> OnAxeHasChanged = new UnityEvent<bool>();

    // parameters
    public int playerHealth;
    public int playerBrevnoCount;


    // keys
    public bool keyOfLesopilkaHas;
       
    // sideQuests
    public bool ballForBoyHas;
    public bool fuelTankHas;

    public bool axeHas;
    
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
        keyOfLesopilkaHas = false;
        ballForBoyHas = false;
        fuelTankHas = false;
        axeHas = false;
        playerBrevnoCount = 0;
        playerHealth = 5;



        OnHealthValueChanged?.Invoke(playerHealth);
        OnBrevnoValueChanged?.Invoke(playerBrevnoCount);
        OnKeyOfLesopilkaHasChanged?.Invoke(keyOfLesopilkaHas);
        OnBallHasChanged?.Invoke(ballForBoyHas);
        OnFuelTankHasChanged?.Invoke(fuelTankHas);
        OnAxeHasChanged?.Invoke(axeHas);
    }

    
    public void GetItem(InteractableItemAsset item)
    {
        string itemType = item.itemType.ToString();
        switch (itemType)
        {

            case "Tree":
                TakeBrevno(item.countOfTree);
                Debug.Log("It's Tree");
                break;

            case "Health":
                Debug.Log("Health");
                TakeHealth(item.valueOfHealth);
                break;
            case "Collactables":
                Debug.Log("Collactables");
                TakeCollactables(item.idOfCollactable);
                break;
            case "Note":
                Debug.Log("Notes");
                TakeNote(item.idOfNote);
                break;
            case "BobrStones":
                Debug.Log("BobrStone");
                TakeBobrStone(item.idOfBobrStone);
                break;
            case "Ability":
                Debug.Log("Ability");
                TakeAbility(item.idOfAblity);
                break;
            case "Chicken":
                Debug.Log("Chicken");
                TakeChicken();
                break;
            



        }
            
    }

    private void TakeDamage(int healtValueChange)
    {
        playerHealth -= healtValueChange;

        OnHealthValueChanged?.Invoke(playerHealth);
        OnDamageTaked?.Invoke();

        if (playerHealth <= 0)
        {
            OnGameOver.Invoke();
        }
    }

    private void TakeHealth(int healthValueChange)
    {
        playerHealth += healthValueChange;
        OnHealthValueChanged?.Invoke(playerHealth);
    }

    private void TakeBrevno(int brevnoValueChange)
    {
        playerBrevnoCount += brevnoValueChange;
        OnBrevnoValueChanged?.Invoke(playerBrevnoCount);
    }

    private void TakeCollactables(int idOfItemCollactables)
    {
        switch (idOfItemCollactables)
        {
            case 0:
                keyOfLesopilkaHas = true;
                OnKeyOfLesopilkaHasChanged?.Invoke(keyOfLesopilkaHas);
                Debug.Log("KeyOfLesopilkaIsTaked");
                break;
            case 1:
                ballForBoyHas = true;
                OnBallHasChanged?.Invoke(ballForBoyHas);
                Debug.Log("BallIsTaked");
                break;
            case 2:
                fuelTankHas = true;
                OnFuelTankHasChanged?.Invoke(fuelTankHas);
                Debug.Log("FuelTaked");
                break;
            case 3:
                axeHas = true;
                OnAxeHasChanged?.Invoke(axeHas);
                Debug.Log("AxeTaked");
                break; 
        }
    }

    private void TakeNote(int idOfNote)
    {
        OnNoteTaked?.Invoke(idOfNote);
    }


    private void TakeBobrStone(int idOfBobrStone)
    {
        OnBobrStoneTaked?.Invoke(idOfBobrStone);
    }

    private void TakeAbility(int idOfAbility)
    {
        OnAbilityTaked?.Invoke(idOfAbility);
    }

    private void TakeChicken()
    {
        OnChickenTaked?.Invoke();
    }

    


}

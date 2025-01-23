using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class PlayerManager : MonoBehaviour
{
    
    public static UnityEvent<int> OnHealthValueChanged = new UnityEvent<int>();
    public static UnityEvent OnGameOver = new UnityEvent();

    public int playerHealth;

    public bool damageTaken;
    public static bool gameOver;
    

    private void OnEnable()
    {
        DamageScript.OnDealingDamage.AddListener(TakeDamage);
    }

    private void OnDisable()
    {
        DamageScript.OnDealingDamage.RemoveListener(TakeDamage);
    }

    void Start()
    {
        playerHealth = 5;
        OnHealthValueChanged.Invoke(playerHealth);
        gameOver = false;
        damageTaken = false;

    }

    void Update()
    {
        

        
    }

    private void TakeDamage(int healtValueChange)
    {
        playerHealth -= healtValueChange;

        OnHealthValueChanged.Invoke(playerHealth);

        if (playerHealth <= 0)
        {
            OnGameOver.Invoke();
        }
    }

    //public IEnumerator Damage(int damageCount)
    //{
    //    while (damageTaken)
    //    {
    //        bloodOverlay.SetActive(true);
    //        playerHealth -= damageCount;
    //        if (playerHealth <= 0)
    //            gameOver = true;

    //        yield return new WaitForSeconds(2f);
    //        bloodOverlay.SetActive(false);

    //    }


    //}
}

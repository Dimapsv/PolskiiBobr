using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int playerHealth;
    public bool damageTaken;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;
    public GameObject bloodOverlay;




    void Start()
    {
        playerHealth = 5;
        gameOver = false;
        damageTaken = false;


    }

    void Update()
    {
        playerHealthText.text = "" + playerHealth;

        if(gameOver)
        {
            SceneManager.LoadScene("Main Scene");
        }
    }

    public IEnumerator Damage(int damageCount)
    {
        while (damageTaken)
        {
            bloodOverlay.SetActive(true);
            playerHealth -= damageCount;
            if (playerHealth <= 0)
                gameOver = true;

            yield return new WaitForSeconds(2f);
            bloodOverlay.SetActive(false);

        }
        

    }
}

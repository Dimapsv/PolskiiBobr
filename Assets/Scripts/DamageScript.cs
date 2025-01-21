using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageCount = 1;
    public PlayerManager playerManager;
    

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerManager.damageTaken = true;
            StartCoroutine(playerManager.Damage(damageCount));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerManager.damageTaken = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageScript : MonoBehaviour
{
    [SerializeField] private int damageCount = 1;
    [SerializeField] private float cooldownDuration = 2f; // Cooldown duration in seconds

    public static UnityEvent<int> OnDealingDamage = new UnityEvent<int>();

    [SerializeField]
    public bool isOnCooldown = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOnCooldown)
        {
            StartCoroutine(DamageWithCooldown(damageCount));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(DamageWithCooldown(damageCount));
        }
    }

    private IEnumerator DamageWithCooldown(int damageCount)
    {
        isOnCooldown = true;
        OnDealingDamage.Invoke(damageCount);
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}

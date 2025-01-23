using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageScript : MonoBehaviour
{
    [SerializeField] private int damageCount = 1;
    
    public static UnityEvent<int> OnDealingDamage = new UnityEvent<int>();
        
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(Damage(damageCount));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StopCoroutine(Damage(damageCount));
    }

    public IEnumerator Damage(int damageCount)
    {
        OnDealingDamage.Invoke(damageCount);
        yield return new WaitForSeconds(2f);
    }
}

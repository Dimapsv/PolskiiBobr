using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthCount;

    public static UnityEvent<int> OnImprovingHealth = new UnityEvent<int>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnImprovingHealth.Invoke(healthCount);
            Destroy(this.gameObject);
        }

    }
}

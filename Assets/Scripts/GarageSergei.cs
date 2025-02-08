using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GarageSergei : MonoBehaviour
{
    public static UnityEvent OnMotoHasChanged = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moto"))
        {
            OnMotoHasChanged?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mommy : MonoBehaviour
{
    public static UnityEvent OnForbiddenBoyHasChanged = new UnityEvent();

    public GameObject boy;

    private void Start()
    {
        boy.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForbiddenBoy"))
        {
            Destroy(other.gameObject);
            boy.SetActive(true);
            OnForbiddenBoyHasChanged?.Invoke();
        }
    }
}

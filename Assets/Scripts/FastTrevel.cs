using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FastTrevel : MonoBehaviour
{

    public static UnityEvent<bool> OnFastTrevelStaying = new UnityEvent<bool>();

    public bool isFastTrevel;

    private void Start()
    {
        isFastTrevel = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFastTrevel = true;
            OnFastTrevelStaying.Invoke(isFastTrevel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFastTrevel = false;
            OnFastTrevelStaying.Invoke(isFastTrevel);
        }
    }
}

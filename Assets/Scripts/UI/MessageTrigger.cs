using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MessageTrigger : MonoBehaviour
{
    public static UnityEvent<int> OnCallMessage = new UnityEvent<int>();

    public int idOfMessage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCallMessage?.Invoke(idOfMessage);
            gameObject.SetActive(false);
        }
    }


}

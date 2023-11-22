using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatinaSctipt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && FindObjectOfType<GameManager>().brevnoIsTaked == true)
        {
            FindObjectOfType<GameManager>().brevnoIsDelivered +=1;
            FindObjectOfType<GameManager>().SpawnBrevno();
        }

    }
}

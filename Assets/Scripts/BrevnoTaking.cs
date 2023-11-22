using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrevnoTaking : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
                FindObjectOfType<GameManager>().brevnoIsTaked = true;
                Destroy(this.gameObject);
           
        }

    }
}

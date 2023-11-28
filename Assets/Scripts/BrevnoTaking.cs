using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrevnoTaking : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
                FindObjectOfType<GameManager>().brevnoIsTaked = true;
                Destroy(GameObject.Find("Visable(Clone)"));
                Destroy(this.gameObject);
                
        }

    }
}

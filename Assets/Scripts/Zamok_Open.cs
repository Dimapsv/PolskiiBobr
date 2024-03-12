using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zamok_Open : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(FindObjectOfType<GameManager>().isKeyLesopilka == true)
            Destroy(this.gameObject);
        }

    }

}

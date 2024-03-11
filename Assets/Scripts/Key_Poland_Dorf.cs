using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Poland_Dorf : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().isKeyPolandDorf = true;
            Destroy(this.gameObject);

        }
    }
}

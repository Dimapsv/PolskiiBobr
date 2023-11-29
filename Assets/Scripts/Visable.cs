using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().isVisableUpgraded = true;
            Destroy(this.gameObject);
        }

    }
}

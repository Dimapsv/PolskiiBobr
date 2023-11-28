using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolandDorf : MonoBehaviour
{
    public Transform[] enemySpawnPoints;
    public GameObject enemyDorf;
    public bool isSpawned = false;
    public GameObject[] obj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isSpawned == false)
        {
            isSpawned = true;
            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                Instantiate(enemyDorf, enemySpawnPoints[i]);
            }

        }
        obj = GameObject.FindGameObjectsWithTag("PolandDorf");


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSpawned = false;
            foreach (GameObject target in obj)
            {
                GameObject.Destroy(target);
            }

        }
    }


}

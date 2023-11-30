using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEnemySpawner1 : MonoBehaviour
{
    public Transform sp;
    public GameObject Enemy;
    public bool isSpawned = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isSpawned == false)
        {
            isSpawned = true;
            Instantiate(Enemy, sp);
        }
    }

    


}

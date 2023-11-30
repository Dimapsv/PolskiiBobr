using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public GameObject[] enmMaze;
    

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            enmMaze = GameObject.FindGameObjectsWithTag("MazeEnemy");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject target in enmMaze)
            {
                GameObject.Destroy(target);
            }
            
        }

    }

    
}

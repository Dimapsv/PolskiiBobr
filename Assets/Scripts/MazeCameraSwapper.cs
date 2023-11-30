using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MazeCameraSwapper : MonoBehaviour
{
    

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject mazeCamera;
    
    
    //public GameObject cameraMaze;


    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(false);
            mazeCamera.SetActive(true);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.SetActive(true);
            mazeCamera.SetActive(false);

        }

    }
}

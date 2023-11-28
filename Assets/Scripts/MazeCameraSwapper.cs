using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCameraSwapper : MonoBehaviour
{
    public GameObject cameraMain;
    public GameObject cameraMaze;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraMain.SetActive(false);
            cameraMaze.SetActive(true);
           
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraMain.SetActive(true);
            cameraMaze.SetActive(false);

        }

    }
}

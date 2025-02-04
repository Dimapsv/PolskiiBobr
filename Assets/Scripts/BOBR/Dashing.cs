using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    //upgrading of dashing
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ThirdPersonController>().isDashingUpgraded = true;
            Destroy(this.gameObject);
        }
            
    }
}

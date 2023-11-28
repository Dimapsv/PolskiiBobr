using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingUpgrade : MonoBehaviour
{
    //upgrading of pushing
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ThirdPersonController>().isPushingUpgraded = true;
            Destroy(this.gameObject);
        }

    }
}

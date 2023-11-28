using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour
{
    //realisation of pushing

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && FindObjectOfType<ThirdPersonController>().isPushingButtonPressed == true && FindObjectOfType<ThirdPersonController>().isPushingUpgraded == true)
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * FindObjectOfType<ThirdPersonController>().forcePush, ForceMode.Impulse);
            }
        }

    }
}

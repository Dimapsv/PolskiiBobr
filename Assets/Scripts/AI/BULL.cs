using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULL : MonoBehaviour
{
    public Rigidbody rb;

    Vector3 initialPosition;
    Quaternion initialRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnOffPhisiks();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BULLBRENCH"))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            rb.velocity = Vector3.zero;
        }
    }


    public void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    

    public void TurnOnPhiciks()
    {
        rb.freezeRotation = true;
        rb.useGravity = true;
        
    }

    public void TurnOffPhisiks()
    {

        rb.useGravity = false;
        rb.freezeRotation = false;
        Invoke("TurnOnPhiciks", 5.0f);
    }
}

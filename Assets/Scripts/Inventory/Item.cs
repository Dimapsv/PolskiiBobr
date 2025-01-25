using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public InteractableItemAsset item;

    public static UnityEvent<InteractableItemAsset> OnItemTaked = new UnityEvent<InteractableItemAsset>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemTaked.Invoke(item);
            Destroy(this.gameObject);
        }

    }

}


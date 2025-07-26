using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Item item;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            if (!Inventory.Instance.HasEmptySlot())
                return;

            Inventory.Instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}

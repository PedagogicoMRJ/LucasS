using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemCollect : MonoBehaviour, IInteractable
{
    public Item item;
    public bool IsInteractable => true;
    public void Interact()
    {
        Collect();
    }
    public void Collect()
    {
        Debug.Log("Collected a " + item.name);
        bool wasCollected = Inventory.instance.Add(item);
        if (wasCollected)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Name Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";

    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}

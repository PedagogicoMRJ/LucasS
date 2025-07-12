using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public int space;
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
        }
        instance = this;
    }
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough space");
                return false;
            }
            items.Add(item);
            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
        }
        return true;
    }

    public void Remove(Item item)
    {
            items.Remove(item);
            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

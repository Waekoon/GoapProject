using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        items.Add(item);
    }

    public GameObject FindItemWithName(string name)
    {
        foreach(GameObject item in items)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }

    public void RemoveItem(GameObject i)
    {
        int indexToRemove = -1;
        bool hasItem = false;
        foreach(GameObject item in items)
        {
            indexToRemove++;
            if (item == i)
            {
                hasItem = true;
            }
        }
        if (hasItem == true) items.RemoveAt(indexToRemove);
    }
}

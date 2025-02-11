using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<Item> _itemsRemaining;
    private int _score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemsRemaining = new List<Item>(GetComponentsInChildren<Item>());

        //Each item starts following the event
        foreach (Item i in _itemsRemaining)
        {
            i.OnPicked += ItemDestroy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HUDManager.SetRemaining(_itemsRemaining.Count);
        HUDManager.SetScore(_score);
    }

    void ItemDestroy(Item item)
    {
        //Stop following the event
        item.OnPicked -= ItemDestroy;
        
        //Delete the item
        _itemsRemaining.Remove(item);
        _score++;
    }
}

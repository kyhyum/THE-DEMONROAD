using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    ItemSlot[] slots;
    Item[] items;
    private int[] count;
    private Inventory inventory;

    private void Awake()
    {
        count = new int[3];
        slots = new ItemSlot[81];
        items = new Item[243];
    }

    public Item[] Get()
    {
        return items;
    }

    public void Set(Item[] items)
    {

    }
}

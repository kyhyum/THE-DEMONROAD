using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotItem
{
    public Item[] items;
    public int gold;
    public SlotItem()
    {

    }
    public SlotItem(int index)
    {
        items = new Item[index];
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemName;
    public string description;
    public Texture2D texture;
    public Rank rank;
    public ItemType type;
    public GameObject prefab;

    public Item(ItemSO itemSO)
    {
        itemName = itemSO.itemName;
        description = itemSO.description;
        texture = itemSO.texture;
        rank = itemSO.rank;
        type = itemSO.type;
        prefab = itemSO.prefab;
    }
}

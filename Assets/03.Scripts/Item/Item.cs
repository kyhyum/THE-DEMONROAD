using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public Texture2D texture;
    public Define.Rank rank;
    public Define.ItemType type;
    public GameObject prefab;
    internal ItemSO itemSO;
    internal int itemPrice;

    public Item(ItemSO itemSO)
    {
        if (itemSO != null)
        {
            itemName = itemSO.itemName;
            description = itemSO.description;
            texture = itemSO.texture;
            rank = itemSO.rank;
            type = itemSO.type;
            prefab = itemSO.prefab;
        }
        else
        {

            Debug.LogError("ItemSO가 null입니다.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public Rank rank;
    public ItemType type;
    public GameObject prefab;

    public void Set(ItemSO itemSO)
    {
        itemName = itemSO.itemName;
        description = itemSO.description;
        icon = itemSO.icon;
        rank = itemSO.rank;
        type = itemSO.type;
        prefab = itemSO.prefab;
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }
}

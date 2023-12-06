using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemLabel : MonoBehaviour
{
    Item item;
    GameObject itemObject;

    public void SetObject(GameObject gameObject)
    {
        itemObject = gameObject;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void GetItem()
    {
        if (UIManager.Instance.GetInventory().AddItem(item))
        {
            if (item.type == Define.ItemType.Gold)
            {

            }
            Destroy(itemObject);
        }
    }
}

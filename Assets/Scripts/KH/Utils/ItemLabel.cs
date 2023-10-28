using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemLabel : MonoBehaviour
{
    public void GetItem()
    {
        Item item = GetComponentInParent<Item>();
        UIManager.Instance.GetInventory().AddItem(item);
        item.RemoveObject();
    }
}

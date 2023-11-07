using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage : MonoBehaviour
{
    [field: SerializeField] private Transform slots;
    private ItemSlot[] storageSlots;
    private int count;
    private int gold = 3000000;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold += value;

            text.text = string.Format("{0:#,###}", gold);
        }
    }
    public TMP_Text text;

    private void Awake()
    {
        count = 0;
        storageSlots = new ItemSlot[81];
    }

    public Item[] Get()
    {
        Item[] items = new Item[81];

        for (int i = 0; i < 81; i++)
        {
            items[i] = storageSlots[i].GetItem();
        }

        return items;
    }

    public void Set(Item[] items)
    {
        for (int i = 0; i < 81; i++)
        {
            storageSlots[i].SetItem(items[i]);
        }
    }
}

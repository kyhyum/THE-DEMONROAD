using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage : MonoBehaviour
{
    [field: SerializeField] private Transform slots;
    private ItemSlot[] storageSlots;
    private int gold;
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

    SlotItem data;
    private void Awake()
    {
        Gold = 0;
        data = new SlotItem(81);
        storageSlots = new ItemSlot[81];

        for (int i = 0; i < 81; i++)
        {
            GameObject slot = Resources.Load<GameObject>("KH/Prefabs/UI/UI_ItemSlot");
            slot = Instantiate(slot, slots);
            slot.name = "StorageSlot" + i;
            storageSlots[i] = slot.AddComponent<StorageSlot>();
            storageSlots[i].slotID = i;
            storageSlots[i].Clear();
        }
    }

    public bool AddItem(Item item)
    {
        if (item.type == ItemType.Gold)
        {
            IStackable stackableItem = (IStackable)item;
            gold += stackableItem.Get();

            return true;
        }
        else if (item is IStackable)
        {
            IStackable stackableItem = (IStackable)item;

            int index = FindItemIndex(item);

            if (index == -1)
            {
                return AddItem(FindIndex(), item);
            }
            else
            {
                storageSlots[index].AddItem(stackableItem.Get());
                return true;
            }
        }
        else
        {
            return AddItem(FindIndex(), item);
        }
    }

    public bool AddItem(int index, Item item)
    {
        if (index == -1)
            return false;

        storageSlots[index].SetItem(item);
        return true;
    }

    public Item GetItem(int slot)
    {
        return storageSlots[slot].GetItem();
    }

    private int FindIndex()
    {
        for (int i = 0; i < 30; i++)
        {
            if (storageSlots[i].GetItem() == null)
            {
                return i;
            }
        }

        return -1;
    }

    private int FindItemIndex(Item item)
    {
        for (int i = 0; i < 30; i++)
        {
            if (item.itemName.Equals(storageSlots[i].GetItem().itemName))
                return i;
        }

        return -1;
    }

    public void SwapItems(int slotA, int slotB)
    {
        // 슬롯 A와 슬롯 B의 아이템 위치를 바꿈
        Item item = storageSlots[slotA].GetItem();
        storageSlots[slotA].SetItem(storageSlots[slotB].GetItem());
        storageSlots[slotB].SetItem(item);
    }

    public SlotItem Get()
    {
        for (int i = 0; i < 81; i++)
        {
            data.items[i] = storageSlots[i].GetItem();
        }

        data.gold = gold;

        return data;
    }

    public void Set(SlotItem data)
    {
        if (data == null)
        {
            return;
        }

        gold = data.gold;

        for (int i = 0; i < 81; i++)
        {
            Item item = data.items[i].itemName.Equals(string.Empty) ? null : data.items[i];

            storageSlots[i].SetItem(item);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] private Transform slots;
    public ItemSlot[] inventorySlots;
    public EquipSlot[] equipSlots;
    private Dictionary<IStackable, int> stackItems;
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
    private int count;
    public TMP_Text text;

    private void Awake()
    {
        count = 0;
        inventorySlots = new ItemSlot[30];
        text.text = string.Format("{0:#,###}", gold);

        for (int i = 0; i < 30; i++)
        {
            GameObject slot = Resources.Load<GameObject>("KH/Prefabs/UI/UI_InventorySlot");
            slot = Instantiate(slot, slots);
            inventorySlots[i] = slot.GetComponent<ItemSlot>();
            inventorySlots[i].slotID = i;
            slot.GetComponentInChildren<InventoryItem>().slotID = i;
            inventorySlots[i].GetComponentInChildren<InventoryItem>().Clear();
        }
    }

    public void Set(ItemSlot[] slots)
    {
        for (int i = 0; i < 30; i++)
        {
            inventorySlots[i] = slots[i];
            if (slots[i].GetComponentInChildren<InventoryItem>().TryGetComponent<Item>(out Item item))
            {
                inventorySlots[i].GetComponentInChildren<InventoryItem>().SetItem(item);
            }
        }
    }

    public bool AddItem(Item item)
    {
        if (count == 30)
            return false;
        InventoryItem inventoryItem;

        if (item.type == ItemType.Gold)
        {
            IStackable stackableItem = (IStackable)item;
            gold += stackableItem.Get();
        }
        else if (item is IStackable)
        {
            IStackable stackableItem = (IStackable)item;
            if (stackItems.TryGetValue(stackableItem, out int index))
            {
                inventoryItem = inventorySlots[index].GetComponentInChildren<InventoryItem>();
                inventoryItem.AddItem(stackableItem.Get());
            }
            else
            {
                stackItems.Add(stackableItem, index);
                AddItem(FindIndex(), item);
            }
        }
        else
        {
            AddItem(FindIndex(), item);
        }

        count++;

        return true;
    }

    public void AddItem(int index, Item item)
    {
        InventoryItem inventoryItem = inventorySlots[index].GetComponentInChildren<InventoryItem>();
        inventorySlots[index].isContain = true;
        inventoryItem.SetItem(item);
    }

    public int FindIndex()
    {
        for (int i = 0; i < 30; i++)
        {
            if (!inventorySlots[i].isContain)
            {
                return i;
            }
        }

        return -1;
    }

    public void SwapItems(int slotA, int slotB)
    {
        // 슬롯 A와 슬롯 B의 아이템 위치를 바꿈
        InventoryItem itemA = inventorySlots[slotA].GetComponentInChildren<InventoryItem>();
        InventoryItem itemB = inventorySlots[slotB].GetComponentInChildren<InventoryItem>();

        itemA.transform.SetParent(inventorySlots[slotB].transform);
        itemB.transform.SetParent(inventorySlots[slotA].transform);
        itemA.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        itemB.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        itemA.slotID = slotB;
        itemB.slotID = slotA;

        bool tmp = inventorySlots[slotA].isContain;
        inventorySlots[slotA].isContain = inventorySlots[slotB].isContain;
        inventorySlots[slotB].isContain = tmp;
    }

    public void Equip(int slotA)
    {
        InventoryItem itemA = inventorySlots[slotA].GetComponentInChildren<InventoryItem>();
        Item item = itemA.GetItem();

        EquipSlot equipSlot = equipSlots[(int)item.type];

        if (equipSlot.TryGetComponent<EquipItem>(out EquipItem equipItem))
        {
            itemA.SetItem(equipItem);
            equipSlot.UnEquip();
        }
        else
        {
            count--;
            inventorySlots[slotA].isContain = false;
            itemA.Clear();
        }

        equipSlot.Equip((EquipItem)item);
    }

    public void UnEquip(int slotA, ItemType type)
    {
        InventoryItem itemA = inventorySlots[slotA].GetComponentInChildren<InventoryItem>();
        EquipSlot equipSlot = equipSlots[(int)type];

        if (itemA.TryGetComponent<Item>(out Item item))
        {
            if (item is EquipItem)
            {
                EquipItem equipItem = (EquipItem)item;

                if (equipItem.type == type)
                {
                    AddItem(slotA, equipSlot.GetItem());
                    equipSlot.UnEquip();
                    equipSlot.Equip(equipItem);

                    return;
                }
            }
            AddItem(equipSlot.GetItem());
            equipSlot.UnEquip();
        }
        else
        {
            AddItem(slotA, equipSlot.GetItem());
            equipSlot.UnEquip();
        }
    }
}

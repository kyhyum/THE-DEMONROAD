using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] private Transform slots;
    private ItemSlot[] inventorySlots;
    public EquipSlot[] equipSlots;
    SlotItem data;
    private int gold;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;

            text.text = string.Format("{0:N0}", gold);
        }
    }
    public TMP_Text text;
    private void Awake()
    {
        Gold = 0;
        data = new SlotItem(37);
        inventorySlots = new ItemSlot[30];

        for (int i = 0; i < 30; i++)
        {
            GameObject slot = Resources.Load<GameObject>("Prefabs/UI/UI_ItemSlot");
            slot = Instantiate(slot, slots);
            slot.name = "InventorySlot" + i;
            inventorySlots[i] = slot.AddComponent<InventorySlot>();
            inventorySlots[i].slotID = i;
            inventorySlots[i].Clear();
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
                inventorySlots[index].AddItem(stackableItem.Get());
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

        inventorySlots[index].SetItem(item);
        return true;
    }

    public Item GetItem(int slot)
    {
        return inventorySlots[slot].GetItem();
    }

    private int FindIndex()
    {
        for (int i = 0; i < 30; i++)
        {
            if (inventorySlots[i].GetItem() == null)
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
            if (inventorySlots[i].GetItem() == null)
                continue;
            if (item.itemName.Equals(inventorySlots[i].GetItem().itemName))
                return i;
        }

        return -1;
    }

    public void SwapItems(int slotA, int slotB)
    {
        // 슬롯 A와 슬롯 B의 아이템 위치를 바꿈
        Item item = inventorySlots[slotA].GetItem();
        inventorySlots[slotA].SetItem(inventorySlots[slotB].GetItem());
        inventorySlots[slotB].SetItem(item);
    }

    public void Equip(int slotA)
    {
        Item item = inventorySlots[slotA].GetItem();
        if (item is EquipItem)
        {
            EquipItem equipItem = (EquipItem)item;
            EquipSlot equipSlot = equipSlots[(int)equipItem.equipType];
            AddItem(slotA, equipSlot.GetItem());
            equipSlot.Equip(item);
        }
    }

    public void UnEquip(int slotA, EquipItemType type)
    {
        EquipSlot equipSlot = equipSlots[(int)type];
        Item item = inventorySlots[slotA].GetItem();
        if (item != null)
        {
            if (item is EquipItem)
            {
                EquipItem equipItem = (EquipItem)item;

                if (equipItem.equipType == type)
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

    public SlotItem Get()
    {
        for (int i = 0; i < 7; i++)
        {
            data.items[i] = equipSlots[i].GetItem();
        }

        for (int i = 0; i < 30; i++)
        {
            data.items[i + 7] = inventorySlots[i].GetItem();
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

        for (int i = 0; i < 37; i++)
        {
            Item item = data.items[i].itemName.Equals(string.Empty) ? null : data.items[i];

            if (i < 7)
            {
                equipSlots[i].SetItem(item);
            }
            else
            {
                inventorySlots[i - 7].SetItem(item);
            }
        }
    }

    public void CloseBtn()
    {
        if (UIManager.Instance.storageOpen)
        {
            UIManager.Instance.ActiveStorage();
        }
        else
        {
            UIManager.Instance.ActiveInventory();
        }
    }

    public void ClickUI()
    {
        UIManager.Instance.ClickInventory();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] private Transform slots;
    public InventorySlot[] inventorySlots;

    private void Awake()
    {
        inventorySlots = new InventorySlot[30];

        for (int i = 0; i < 30; i++)
        {
            GameObject slot = Resources.Load<GameObject>("KH/Prefabs/UI/InventorySlot");
            slot = Instantiate(slot, slots);
            inventorySlots[i] = slot.GetComponent<InventorySlot>();
            inventorySlots[i].slotID = i;
            slot.GetComponentInChildren<InventoryItem>().slotID = i;
            slot.GetComponentInChildren<TMP_Text>().text = i.ToString();
        }
    }

    public void Set(InventorySlot[] slots)
    {
        for (int i = 0; i < 30; i++)
        {
            inventorySlots[i] = slots[i];
        }
    }

    public void AddItem(Item item)
    {

    }

    public void SwapItems(int slotA, int slotB)
    {
        // 슬롯 A와 슬롯 B의 아이템 위치를 바꿈
        InventoryItem itemA = inventorySlots[slotA].GetComponentInChildren<InventoryItem>();
        InventoryItem itemB = inventorySlots[slotB].GetComponentInChildren<InventoryItem>();

        if (itemA != null && itemB != null)
        {
            itemA.transform.SetParent(inventorySlots[slotB].transform);
            itemB.transform.SetParent(inventorySlots[slotA].transform);
            itemA.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            itemB.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            itemA.slotID = slotB;
            itemB.slotID = slotA;
        }
    }
}

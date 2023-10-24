using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] private Transform slots;
    ItemSlotUI[] itemSlots;

    private void Awake()
    {
        itemSlots = new ItemSlotUI[30];

        for (int i = 0; i < 30; i++)
        {
            GameObject slot = Resources.Load<GameObject>("KH/Prefabs/UI/ItemSlot");
            Instantiate(slot, slots);
            itemSlots[i] = slot.GetComponent<ItemSlotUI>();
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Set(ItemSlotUI[] slots)
    {
        for (int i = 0; i < 30; i++)
        {
            itemSlots[i] = slots[i];
        }
    }
}

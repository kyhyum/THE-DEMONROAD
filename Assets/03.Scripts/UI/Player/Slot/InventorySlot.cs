using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : ItemSlot, IDropHandler, IPointerDownHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        // 이벤트 데이터를 이용해 드롭 지점에서의 Raycast를 수행
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent<QuickSlot>(out QuickSlot quickSlot))
            {
                Item item = GetItem();

                Debug.Log(item);
                if (item is UseItem)
                {
                    Debug.Log(item);
                    quickSlot.SetSlot((UseItem)item);
                }
                return;
            }

            if (result.gameObject.TryGetComponent<InventorySlot>(out InventorySlot inventorySlot))
            {
                if (inventorySlot.slotID == slotID)
                    continue;
                UIManager.Instance.GetInventory().SwapItems(inventorySlot.slotID, slotID);
                return;
            }

            if (result.gameObject.TryGetComponent<EquipSlot>(out EquipSlot equipSlot))
            {
                UIManager.Instance.GetInventory().Equip(slotID);
                return;
            }

            if (result.gameObject.TryGetComponent<StorageSlot>(out StorageSlot storageSlot))
            {
                UIManager.Instance.SwapItems(slotID, storageSlot.slotID);
                return;
            }

            if (result.gameObject.TryGetComponent<Shop>(out Shop shop))
            {
                shop.ShowConfirmationPop();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Item item = GetItem();

        if (item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (UIManager.Instance.storageOpen)
            {
                if (UIManager.Instance.GetStorage().AddItem(item))
                {
                    SetItem(null);
                }
            }
            else if (item is EquipItem)
            {
                UIManager.Instance.GetInventory().Equip(slotID);
            }
            else if (item is UseItem)
            {
                UseItem useItem = (UseItem)item;
                useItem.Use();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : ItemSlot, IDropHandler, IPointerDownHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // 드롭 대상의 RectTransform을 얻음
        RectTransform dropTarget = this.transform as RectTransform;

        // 이벤트 데이터를 이용해 드롭 지점에서의 Raycast를 수행
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log(result);
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

                return;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetItem() != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (UIManager.Instance.storageOpen)
            {
                // UIManager.Instance.GetStorage().AddItem();
            }
            else if (GetItem() is EquipItem)
            {
                UIManager.Instance.GetInventory().Equip(slotID);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerDownHandler
{
    public Define.EquipItemType type;
    private GameObject itemClone;
    private Canvas canvas;
    private RectTransform rect;
    public RawImage icon;
    private Item item;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        SetItem(null);
    }

    public void SetItem(Item item)
    {
        this.item = item;

        if (item == null)
        {
            SetAlpha(0);
            return;
        }

        icon.texture = item.texture;
        SetAlpha(1);
    }

    public Item GetItem()
    {
        return item;
    }


    private void SetAlpha(float f)
    {
        Color color = icon.color;
        color.a = f;
        icon.color = color;
    }

    public void Equip(Item item)
    {
        if (item is EquipItem)
        {
            EquipItem equipItem = (EquipItem)item;

            equipItem.Equip();
        }

        SetItem(item);
    }

    public void UnEquip()
    {
        Item item = GetItem();

        if (item is EquipItem)
        {
            EquipItem equipItem = (EquipItem)item;

            equipItem.UnEquip();
        }

        SetItem(null);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null)
            return;
        itemClone = Instantiate(gameObject, canvas.GetComponent<Transform>());
        rect = itemClone.GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(70, 70);

        Vector2 mousePosition = Input.mousePosition;

        rect.anchoredPosition = mousePosition;

        EquipSlot slot = itemClone.GetComponent<EquipSlot>();
        slot.SetItem(item);
        slot.type = type;

        RawImage image = itemClone.GetComponentInChildren<RawImage>();
        Color color = image.color;
        color.a = .8f;
        image.color = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemClone == null)
            return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemClone == null)
            return;
        Destroy(itemClone);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드롭 대상의 RectTransform을 얻음
        RectTransform dropTarget = this.transform as RectTransform;

        // 이벤트 데이터를 이용해 드롭 지점에서의 Raycast를 수행
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent<InventorySlot>(out InventorySlot inventorySlot))
            {
                UIManager.Instance.GetInventory().UnEquip(inventorySlot.slotID, type);
                return;
            }

            if (result.gameObject.TryGetComponent<StorageSlot>(out StorageSlot storageSlot))
            {
                UIManager.Instance.GetStorage();
                return;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Item item = GetItem();
        if (item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (UIManager.Instance.GetInventory().AddItem(item))
            {
                UnEquip();
            }
        }
    }
}

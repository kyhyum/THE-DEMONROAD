using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerDownHandler
{
    public int slotID;
    private GameObject itemClone;
    private Canvas canvas;
    private RectTransform rect;
    private RawImage icon;
    private Item item;
    private TMP_Text quantity;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        icon = GetComponentInChildren<RawImage>();
        quantity = GetComponentInChildren<TMP_Text>();
        item = null;
    }

    public void SetItem(Item item)
    {

        if (item.type == ItemType.Resources || item.type == ItemType.Gold)
        {
            this.item = gameObject.AddComponent<ResourceItem>();
        }
        else if (item.type == ItemType.Consumes)
        {
            this.item = gameObject.AddComponent<UseItem>();
        }
        else
        {
            this.item = gameObject.AddComponent<EquipItem>();
        }

        this.item.Set(item);

        icon.texture = item.texture;

        if (item is IStackable)
        {
            IStackable stackableItem = (IStackable)item;
            quantity.text = stackableItem.Get().ToString();
        }
        else
        {
            quantity.text = string.Empty;
        }

        Color color = icon.color;
        color.a = 1;
        icon.color = color;
    }

    public void AddItem(int count)
    {
        if (item is IStackable)
        {
            IStackable stackableItem = (IStackable)item;
            stackableItem.Add(count);
            quantity.text = stackableItem.Get().ToString();
        }
    }

    public void Clear()
    {
        item = null;
        Color color = icon.color;
        color.a = 0;
        icon.color = color;
        quantity.text = string.Empty;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (icon.color.a == 0)
            return;
        itemClone = Instantiate(gameObject, canvas.GetComponent<Transform>());
        rect = itemClone.GetComponent<RectTransform>();

        Vector2 mousePosition = Input.mousePosition;
        mousePosition.x -= Screen.width / 2;
        mousePosition.y -= Screen.height / 2;

        rect.anchoredPosition = mousePosition;

        RawImage image = itemClone.GetComponentInChildren<RawImage>();
        Color color = image.color;
        color.a = .8f;
        image.color = color;

        TMP_Text text = itemClone.GetComponentInChildren<TMP_Text>();
        color = text.color;
        color.a = .8f;
        text.color = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (icon.color.a == 0)
            return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (icon.color.a == 0)
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
            if (result.gameObject.TryGetComponent<InventoryItem>(out InventoryItem item))
            {
                if (item.slotID == slotID)
                    continue;
                UIManager.Instance.GetInventory().SwapItems(item.slotID, slotID);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (icon.color.a != 0 && eventData.button == PointerEventData.InputButton.Right)
        {
            // 우클릭 이벤트 처리 코드를 여기에 작성합니다.
            Debug.Log("우클릭 발생!");
        }
    }
}

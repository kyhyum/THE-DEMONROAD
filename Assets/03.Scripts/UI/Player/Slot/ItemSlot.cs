using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject itemClone;
    private Canvas canvas;
    private RectTransform rect;
    private RawImage icon;
    private Item item;
    private TMP_Text quantity;
    public int slotID;
    private bool isClone;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        icon = GetComponentInChildren<RawImage>();
        quantity = GetComponentInChildren<TMP_Text>();
        isClone = false;
        SetItem(null);
    }

    public void SetItem(Item newItem)
    {
        if (item is UseItem)
        {
            UseItem useItem = (UseItem)item;
            useItem.OnCountChanged -= SetQuantity;
        }

        if (newItem == null)
        {
            Clear();
            item = null;
            return;
        }

        item = newItem;

        if (item is IStackable)
        {
            SetQuantity(((IStackable)item).Get());
        }
        else
        {
            quantity.text = string.Empty;
        }

        if (item is UseItem && !isClone)
        {
            UseItem useItem = (UseItem)item;
            useItem.OnCountChanged += SetQuantity;
        }

        icon.texture = item.texture;
        SetAlpha(1);
    }

    public Item GetItem()
    {
        return item;
    }

    public void AddItem(int count)
    {
        Item item = GetItem();

        if (item is IStackable)
        {
            IStackable stackableItem = (IStackable)item;
            stackableItem.Add(count);
            SetItem((Item)stackableItem);
        }
    }

    public void Clear()
    {
        quantity.text = string.Empty;
        SetAlpha(0);
    }

    public void SetAlpha(float f)
    {
        Color color = icon.color;
        color.a = f;
        icon.color = color;
    }

    public void SetQuantity(int count)
    {
        if (count == 0)
        {
            SetItem(null);
            return;
        }

        quantity.text = count.ToString();
    }

    public void SetClone()
    {
        isClone = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null)
            return;
        itemClone = Instantiate(gameObject, canvas.GetComponent<Transform>());
        rect = itemClone.GetComponent<RectTransform>();
        ItemSlot itemSlot = itemClone.GetComponent<ItemSlot>();
        itemSlot.SetClone();
        itemSlot.SetItem(item);

        rect.sizeDelta = new Vector2(70, 70);

        Vector2 mousePosition = Input.mousePosition;

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
}

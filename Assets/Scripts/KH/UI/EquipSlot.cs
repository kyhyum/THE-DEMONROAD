using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public ItemType type;
    private EquipItem item;
    private RawImage icon;
    private Texture2D baseImage;
    private GameObject itemClone;
    private Canvas canvas;
    private RectTransform rect;
    private bool isEquip;
    private bool isDrag;

    private void Awake()
    {
        icon = GetComponent<RawImage>();
        baseImage = Resources.Load<Texture2D>("KH/Images/UI/Equip/" + gameObject.name);
        Clear();
    }

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    private void Clear()
    {
        icon.texture = baseImage;
        icon.color = Color.black;
        isEquip = false;
    }

    public void Equip(EquipItem item)
    {
        this.item = gameObject.AddComponent<EquipItem>();
        this.item.Set(item);

        icon.color = Color.white;
        icon.texture = item.texture;
        isEquip = true;
        item.Equip();
    }

    public void UnEquip()
    {
        Clear();
        item.UnEquip();
        isEquip = false;
        Destroy(item);
    }

    public Item GetItem()
    {
        return item;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TryGetComponent<EquipItem>(out EquipItem item) && eventData.button == PointerEventData.InputButton.Right)
        {
            if (UIManager.Instance.GetInventory().AddItem(item))
            {
                UnEquip();
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isEquip)
            return;
        isDrag = true;

        itemClone = Instantiate(gameObject, canvas.GetComponent<Transform>());
        rect = itemClone.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(70, 70);

        if (itemClone.TryGetComponent<EquipSlot>(out EquipSlot equipSlot))
        {
            equipSlot.Equip(item);
            equipSlot.type = type;
        }

        Vector2 mousePosition = Input.mousePosition;

        rect.anchoredPosition = mousePosition;

        RawImage image = itemClone.GetComponentInChildren<RawImage>();
        Color color = image.color;
        color.a = .8f;
        image.color = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDrag)
            return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDrag)
            return;
        Destroy(itemClone);
        isDrag = false;
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
            if (result.gameObject.TryGetComponent<ItemSlot>(out ItemSlot slot))
            {
                UIManager.Instance.GetInventory().UnEquip(slot.slotID, item.type);
            }
        }
    }
}

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

    private void Start()
    {
        icon = GetComponent<RawImage>();
        canvas = GetComponentInParent<Canvas>();
        baseImage = Resources.Load<Texture2D>("KH/Images/UI/Equip/" + gameObject.name);
        Clear();
    }

    private void Clear()
    {
        icon.texture = baseImage;
        icon.color = Color.black;
    }

    public void Equip(EquipItem item)
    {
        this.item = gameObject.AddComponent<EquipItem>();
        this.item.Set(item);
        icon.color = Color.white;
        item.Equip();

        icon.texture = item.texture;
    }

    public void UnEquip()
    {
        Clear();
        item.UnEquip();
        Destroy(item);
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
        if (icon.texture == baseImage)
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (icon.texture == baseImage)
            return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (icon.texture == baseImage)
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
            if (result.gameObject.TryGetComponent<InventoryItem>(out InventoryItem inventoryItem))
            {
                Debug.Log("OnDrop");
            }
        }
    }
}

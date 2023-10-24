using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Canvas canvas;
    private RectTransform rect;
    private Vector2 defaultPos;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = rect.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rect.anchoredPosition = defaultPos;
    }
}

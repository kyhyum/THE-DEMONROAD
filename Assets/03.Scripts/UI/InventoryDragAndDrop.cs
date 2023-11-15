using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragAndDrop : MonoBehaviour, IDragHandler
{
    [field: SerializeField] private RectTransform rect;
    private Canvas canvas;
    private float canvasWidth;
    private float canvasHeight;
    private float width;
    private float height;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        width = rect.rect.width / 2;
        height = rect.rect.height / 2;
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width / 2;
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height / 2;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if (rect.anchoredPosition.x > canvasWidth - width)
        {
            rect.anchoredPosition = new Vector2(canvasWidth - width, rect.anchoredPosition.y);
        }

        if (rect.anchoredPosition.x < -(canvasWidth - width))
        {
            rect.anchoredPosition = new Vector2(-(canvasWidth - width), rect.anchoredPosition.y);
        }

        if (rect.anchoredPosition.y > canvasHeight - height)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, canvasHeight - height);
        }

        if (rect.anchoredPosition.y < -(canvasHeight - height))
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -(canvasHeight - height));
        }
    }
}

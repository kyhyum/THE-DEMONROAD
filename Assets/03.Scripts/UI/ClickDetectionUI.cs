using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetectionUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler
{
    private CanvasRenderer canvasRenderer;

    private void Awake()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI Element Clicked: " + gameObject.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("UI Element Clicked: " + gameObject.name);
    }
}

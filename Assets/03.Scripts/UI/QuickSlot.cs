using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private GameObject itemClone;
    private Canvas canvas;
    private RectTransform rect;
    private IUsable usable;
    [field: SerializeField] private RawImage icon;
    [field: SerializeField] private TMP_Text keyBinding;
    [field: SerializeField] private TMP_Text quantity;
    [field: SerializeField] private InputActionReference inputActionReference;
    public int slotID;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        SetSlot(null);
    }

    private void Start()
    {
        keyBinding.text = InputManager.GetBindingName(inputActionReference.action.name);
        inputActionReference.action.Enable();
        inputActionReference.action.started += Use;
    }

    public void SetSlot(IUsable usable)
    {
        this.usable = usable;

        if (usable == null)
        {
            SetAlpha(0);
            return;
        }

        if (usable is IStackable)
        {
            IStackable stackable = (IStackable)usable;
            quantity.text = stackable.Get().ToString();
        }

        if (usable is UseItem)
        {
            UseItem useItem = (UseItem)usable;
            useItem.OnCountChanged += SetQuantity;
            icon.texture = useItem.texture;
        }



        SetAlpha(1);
    }

    private void SetAlpha(float f)
    {
        Color color = icon.color;
        color.a = f;
        icon.color = color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (usable == null)
            return;
        itemClone = Instantiate(gameObject, canvas.GetComponent<Transform>());
        rect = itemClone.GetComponent<RectTransform>();

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

    public void OnDrop(PointerEventData eventData)
    {
        // 이벤트 데이터를 이용해 드롭 지점에서의 Raycast를 수행
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent<QuickSlot>(out QuickSlot quickSlot))
            {

                return;
            }
        }

        SetSlot(null);
    }

    private void SetQuantity(int count)
    {
        quantity.text = count.ToString();
    }

    private void Use(InputAction.CallbackContext context)
    {
        Debug.Log("QuickSlot" + slotID + "Clicked");
        if (usable == null)
            return;

        if (usable is IStackable)
        {
            IStackable stackable = (IStackable)usable;

            if (stackable.Get() == 0)
                return;

            usable.Use();
        }
        else
        {

        }
    }
}

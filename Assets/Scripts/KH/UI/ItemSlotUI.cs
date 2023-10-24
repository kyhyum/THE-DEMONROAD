using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [field: SerializeField] private Image icon;
    private Item item;
    private TMP_Text quantity;

    private void Awake()
    {
        quantity = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        // Clear();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        icon.sprite = item.data.icon;

        if (item is IStackable)
        {
            quantity.text = ((IStackable)item).Get().ToString();
        }
        else
        {
            quantity.text = null;
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantity.text = string.Empty;
    }
}

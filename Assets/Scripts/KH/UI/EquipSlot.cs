using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public ItemType type;
    private EquipItem item;
    private RawImage icon;
    private Texture2D baseImage;

    private void Start()
    {
        icon = GetComponentInChildren<RawImage>();
        baseImage = Resources.Load<Texture2D>("KH/Images/UI/Equip/" + gameObject.name);
        Clear();
    }

    private void Clear()
    {
        icon.texture = baseImage;
    }
}

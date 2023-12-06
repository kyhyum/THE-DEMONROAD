using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string itemName { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public Define.ItemType type { get; set; }
    [field: SerializeField] public Define.Rank rank { get; private set; }
    [field: SerializeField] public Texture2D texture { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public int itemPrice { get; private set; }
    [field: SerializeField] public AudioClip audioClip { get; private set; }


    public virtual GameObject CreateItem()
    {
        GameObject itemObj = Instantiate(prefab);
        Item item;

        if (type == Define.ItemType.Resources || type == Define.ItemType.Gold)
        {
            item = new ResourceItem(this);
        }
        else if (type == Define.ItemType.Consumes)
        {
            item = new UseItem(this);
        }
        else
        {
            item = new EquipItem(this);
        }

        GameObject canvas = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UI_ItemLabel"), itemObj.transform);
        TMP_Text text = canvas.GetComponentInChildren<TMP_Text>();
        text.text = itemName;

        Image image = canvas.GetComponentInChildren<Image>();
        Color imageColor = new Color();
        Color textColor = new Color();

        canvas.GetComponentInChildren<ItemLabel>().SetItem(item);
        canvas.GetComponentInChildren<ItemLabel>().SetObject(itemObj);

        switch (rank)
        {
            case Define.Rank.Common:
                imageColor = new Color(255, 255, 255);
                textColor = new Color(255, 255, 255);
                break;
            case Define.Rank.Rare:
                imageColor = new Color(0, 255, 0);
                textColor = new Color(0, 255, 0);
                break;
            case Define.Rank.Epic:
                imageColor = new Color(128, 0, 255);
                textColor = new Color(128, 0, 255);
                break;
            case Define.Rank.Legend:
                imageColor = new Color(255, 255, 0);
                textColor = new Color(255, 255, 0);
                break;
        }

        imageColor.a = .3f;
        textColor.a = 1;

        image.color = imageColor;
        text.color = textColor;

        return itemObj;
    }
}

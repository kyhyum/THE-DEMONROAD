using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string itemName { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public ItemType type { get; set; }
    [field: SerializeField] public Rank rank { get; private set; }
    [field: SerializeField] public Texture2D texture { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public int itemPrice { get; private set; }
 

    public virtual GameObject CreateItem()
    {
        GameObject gameObject = Instantiate(prefab);
        Item item;

        if (type == ItemType.Resources || type == ItemType.Gold)
        {
            item = new ResourceItem(this);
        }
        else if (type == ItemType.Consumes)
        {
            item = new UseItem(this);
        }
        else
        {
            item = new EquipItem(this);
        }

        GameObject canvas = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UI_ItemLabel"), gameObject.transform);
        TMP_Text text = canvas.GetComponentInChildren<TMP_Text>();
        text.text = itemName;

        Image image = canvas.GetComponentInChildren<Image>();
        Color imageColor = new Color();
        Color textColor = new Color();

        canvas.GetComponentInChildren<ItemLabel>().SetItem(item);
        canvas.GetComponentInChildren<ItemLabel>().SetObject(gameObject);

        switch (rank)
        {
            case Rank.Common:
                imageColor = new Color(255, 255, 255);
                textColor = new Color(255, 255, 255);
                break;
            case Rank.Rare:
                imageColor = new Color(0, 255, 0);
                textColor = new Color(0, 255, 0);
                break;
            case Rank.Epic:
                imageColor = new Color(128, 0, 255);
                textColor = new Color(128, 0, 255);
                break;
            case Rank.Legend:
                imageColor = new Color(255, 255, 0);
                textColor = new Color(255, 255, 0);
                break;
        }

        imageColor.a = .3f;
        textColor.a = 1;

        image.color = imageColor;
        text.color = textColor;

        return gameObject;
    }
}

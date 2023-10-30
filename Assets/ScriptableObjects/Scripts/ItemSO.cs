using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Item", menuName = "Create/Item", order = 0)]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string itemName { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public ItemType type { get; private set; }
    [field: SerializeField] public Rank rank { get; private set; }
    [field: SerializeField] public Texture2D texture { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
    public Sprite icon { get; private set; }

    public void ConvertToSprite()
    {
        // Texture2D를 Sprite로 변환
        icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }

    public GameObject CreateItem()
    {
        ConvertToSprite();

        GameObject gameObject = Instantiate(prefab);
        Item item;

        if (type == ItemType.Resources || type == ItemType.Gold)
        {
            item = gameObject.AddComponent<ResourceItem>();
        }
        else if (type == ItemType.Consumes)
        {
            item = gameObject.AddComponent<UseItem>();
        }
        else
        {
            item = gameObject.AddComponent<EquipItem>();
        }

        item.Set(this);

        GameObject canvas = Instantiate(Resources.Load<GameObject>("KH/Prefabs/UI/ItemLabel"), item.transform);
        TMP_Text text = canvas.GetComponentInChildren<TMP_Text>();
        text.text = itemName;

        Image image = canvas.GetComponentInChildren<Image>();
        Color imageColor = new Color();
        Color textColor = new Color();

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

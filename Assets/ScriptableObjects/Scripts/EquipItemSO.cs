using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option
{
    public StatType type;
    public int value;
}

[CreateAssetMenu(fileName = "EquipItem", menuName = "Create/EquipItem", order = 0)]
public class EquipItemSO : ItemSO
{
    public EquipItemType equipType;
    public List<Option> options;

    public override GameObject CreateItem()
    {
        Debug.Log("aaa");

        return base.CreateItem();
    }
}
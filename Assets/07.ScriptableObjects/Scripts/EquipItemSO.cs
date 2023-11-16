using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EquipItem", menuName = "Create/EquipItem", order = 0)]
public class EquipItemSO : ItemSO
{
    public EquipItemType equipType;
    public List<Option> options;

    public override GameObject CreateItem()
    {

        GameObject gameObject = base.CreateItem();


        return gameObject;
    }
}
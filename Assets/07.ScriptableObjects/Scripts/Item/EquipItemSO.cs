using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItem", menuName = "Item/EquipItem")]
public class EquipItemSO : ItemSO
{
    public Define.EquipItemType equipType;
    public List<Option> options;
    public float Attack;
    public float Deffence;

    public override GameObject CreateItem()
    {

        GameObject itemObj = base.CreateItem();
        itemObj.GetComponentInChildren<ItemLabel>().SetItem(new EquipItem(this));

        return itemObj;
    }
}
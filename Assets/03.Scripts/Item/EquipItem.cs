using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : Item
{
    public EquipItemType equipType;
    public List<Option> options;
    public float Attack;
    public float Deffence;
    public EquipItem(ItemSO itemSO) : base(itemSO)
    {
        options = new List<Option>();

        if (itemSO is EquipItemSO)
        {
            EquipItemSO equipItemSO = (EquipItemSO)itemSO;
            equipType = equipItemSO.equipType;

            foreach (Option option in equipItemSO.options)
            {
                options.Add(option);
            }

            Attack = equipItemSO.Attack;
            Deffence = equipItemSO.Deffence;
        }
    }

    public void Equip()
    {

    }

    public void UnEquip()
    {

    }
}

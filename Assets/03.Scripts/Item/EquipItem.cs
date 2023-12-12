using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : Item
{
    public Define.EquipItemType equipType;
    public List<Option> options;
    public float attack;
    public float deffence;
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

            attack = equipItemSO.attack;
            deffence = equipItemSO.deffence;
        }
    }

    public void Equip()
    {

    }

    public void UnEquip()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemSO : ItemSO
{
    public int count;


    public override GameObject CreateItem()
    {
        GameObject gameObject = base.CreateItem();
        gameObject.GetComponentInChildren<ItemLabel>().SetItem(new UseItem(this));

        return gameObject;
    }

}
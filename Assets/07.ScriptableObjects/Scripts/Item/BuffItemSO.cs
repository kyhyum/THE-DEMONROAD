using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffItem", menuName = "Item/BuffItem")]
public class BuffItemSO : UseItemSO
{
    public Define.BuffType buffType;
    public float duration;
    public int value;

    public override GameObject CreateItem()
    {

        GameObject gameObject = base.CreateItem();

        gameObject.GetComponentInChildren<ItemLabel>().SetItem(new BuffItem(this));

        return gameObject;
    }
}
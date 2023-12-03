using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestoreItem", menuName = "Item/RestoreItem")]
public class RestoreItemSO : UseItemSO
{
    public Define.RestoreType restoreType;
    public float value;

    public override GameObject CreateItem()
    {

        GameObject itemObj = base.CreateItem();

        itemObj.GetComponentInChildren<ItemLabel>().SetItem(new RestoreItem(this));

        return itemObj;
    }
}
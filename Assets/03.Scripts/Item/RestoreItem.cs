using UnityEngine;

public class RestoreItem : UseItem
{
    Define.RestoreType restoreType;
    float value;

    public RestoreItem(ItemSO itemSO) : base(itemSO)
    {
        if (itemSO is RestoreItemSO)
        {
            RestoreItemSO restoreItemSO = (RestoreItemSO)itemSO;
            restoreType = restoreItemSO.restoreType;
            value = restoreItemSO.value;
        }
    }

    public override void Use()
    {
        GameManager.Instance.condition.Restore(restoreType, value);
        Debug.Log(restoreType + ": " + value);
        base.Use();
    }
}
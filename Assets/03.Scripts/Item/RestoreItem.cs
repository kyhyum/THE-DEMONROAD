public class RestoreItem : UseItem
{
    RestoreType restoreType;
    float value;

    public RestoreItem(ItemSO itemSO) : base(itemSO)
    {
        if (itemSO is RestoreItemSO)
        {
            RestoreItemSO restoreItemSO = (RestoreItemSO)itemSO;
            restoreType = restoreItemSO.restoreType;
        }
    }

    public override void Use()
    {
        PlayerCondition condition = GameManager.Instance.player.conditon;

        switch (restoreType)
        {
            case RestoreType.HP:
                condition.currentHp = condition.currentHp + value > condition.maxHp ? condition.maxHp : condition.currentHp + value;
                break;
            case RestoreType.MP:
                condition.currentHp = condition.currentMp + value > condition.maxMp ? condition.maxMp : condition.currentMp + value;
                break;
        }

        base.Use();
    }
}
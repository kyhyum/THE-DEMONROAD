[System.Serializable]
public class QuickSlotData
{
    public Define.QuickSlotType type;
    public int index;

    public QuickSlotData(Define.QuickSlotType type, int index)
    {
        this.type = type;
        this.index = index;
    }
}
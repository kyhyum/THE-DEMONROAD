public class Define
{
    public enum ItemType
    {
        Equip,
        Consumes,
        Resources,
        Gold
    }

    public enum EquipItemType
    {
        Helmet,
        Armor,
        Gloves,
        Boots,
        Weapon,
        Necklace,
        Ring
    }

    public enum UseItemType
    {
        Restore,
        Buff
    }

    public enum BuffType
    {
        Atk,
        Def,
        Str,
        Dex,
        Int,
        Con
    }

    public enum RestoreType
    {
        HP,
        MP
    }

    public enum Rank
    {
        Common,
        Rare,
        Epic,
        Legend
    }

    public enum SkillType
    {
        Attack, Buff
    }

    public enum Job
    {
        WARRIOR,
        ARCHOR,
        WIZZARD
    }

    public enum SceneType
    {
        Start,
        Loading,
        Tutorial,
        Town,
        Dungeon
    }

    public enum StatType
    {
        STR,
        DEX,
        INT,
        CON
    }

    public enum QuestType
    {
        MonsterQuest,
        ItemQuest,
        ConversationQuest,
        InfiniteMonsterQuest,
        MainQuest,
    }
}
using UnityEngine;

public class Skill
{
    public int index;
    public string skillName;
    public string description;
    public Sprite icon;
    public int level;
    public int manaCost;
    public float coolTime;

    public Skill(SkillSO skillSO)
    {
        skillName = skillSO.skillName;
        description = skillSO.description;
        icon = skillSO.icon;
        manaCost = skillSO.manaCost;
        coolTime = skillSO.coolTime;
    }

    public void LevelUp()
    {
        level++;
    }

    public void LevelDown()
    {
        level--;
    }
}

using UnityEngine;

public class Skill
{
    public int index;
    public string skillName;
    public string description;
    public Sprite icon;
    public int level;
    public int manaCost;
    public float cooltime;

    public Skill(SkillSO skillSO)
    {
        skillName = skillSO.skillName;
        description = skillSO.description;
        icon = skillSO.icon;
        manaCost = skillSO.manaCost;
        cooltime = skillSO.cooltime;
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

using UnityEngine;

public class Skill
{
    public int index;
    public string skillName;
    public string description;
    public int level;
    public int manaCost;
    public float coolTime;

    public Skill(SkillSO skillSO)
    {
        index = skillSO.index;
        skillName = skillSO.skillName;
        description = skillSO.description;
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

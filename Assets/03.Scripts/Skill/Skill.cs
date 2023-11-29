public class Skill
{
    public string skillName;
    public string description;
    public int level;
    public int damage;
    public int increasedDamagePerLevel;
    public float stunTime;
    public float increasedStunTimePerLevel;
    public int manaCost;
    public float coolTime;

    public void Set(SkillSO skillSO)
    {
        skillName = skillSO.skillName;
        damage = skillSO.damage;
        increasedDamagePerLevel = skillSO.increasedDamagePerLevel;
        stunTime = skillSO.stunTime;
        increasedStunTimePerLevel = skillSO.increasedStunTimePerLevel;
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

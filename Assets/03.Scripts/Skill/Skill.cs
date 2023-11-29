public class Skill
{
    public string skillName;
    public string description;
    public int level;
    public int damage;
    public int increasedDamagePerLevel;
    public int manaCost;
    public float coolTime;

    public void Set(SkillSO skillSO)
    {
        skillName = skillSO.skillName;
        damage = skillSO.damage;
        increasedDamagePerLevel = skillSO.increasedDamagePerLevel;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string skillName;
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
}

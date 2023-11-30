using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSkill", menuName = "Skill/AttackSkill")]
public class AttackSkillSO : SkillSO
{
    [field: SerializeField] public int damage;
    [field: SerializeField] public int increasedDamagePerLevel;
}
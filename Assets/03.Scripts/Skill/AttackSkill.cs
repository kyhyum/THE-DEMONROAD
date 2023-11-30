using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill, IUsable
{

    public int damage;
    public int increasedDamagePerLevel;

    public AttackSkill(SkillSO skillSO) : base(skillSO)
    {
        if (skillSO is AttackSkillSO)
        {
            AttackSkillSO attackSkillSO = (AttackSkillSO)skillSO;

            damage = attackSkillSO.damage;
            increasedDamagePerLevel = attackSkillSO.increasedDamagePerLevel;
        }
    }


    // TODO: 이펙트, 범위, 대미지
    public void Use()
    {
        Player player = GameManager.Instance.player;
        if (player.IsAttack())
            return;

        player.IsAttackSkill[index] = true;
    }
}
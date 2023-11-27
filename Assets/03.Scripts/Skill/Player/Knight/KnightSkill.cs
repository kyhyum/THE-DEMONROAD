using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    [field: SerializeField] SkillSO strikeSkillSO;
    [field: SerializeField] SkillSO shieldStrikeSO;
    [field: SerializeField] SkillSO whirlingCleaveSO;

    string strikeSkillName = "Strike";
    string shieldStrikeSkillName = "ShieldStrike";
    string whirlingCleaveSkillName = "WhirlingCleave";

    //public int level;
    //public int damage;
    //public int increasedDamagePerLevel;
    //public float stunTime;
    //public float increasedStunTimePerLevel;
    //public int manaCost;
    //public float coolTime;

    private void Start()
    {
        Use(strikeSkillSO, 1);
        Use(shieldStrikeSO, 2);
        Use(whirlingCleaveSO, 3);
    }

    public void Use(SkillSO skillSO, int level)
    {
        if (skillSO.skillName == strikeSkillName)
        {
            Strike(level);
        }
        else if (skillSO.skillName == shieldStrikeSkillName)
        {
            ShieldStrike(level);
        }
        else if (skillSO.skillName == whirlingCleaveSkillName)
        {
            WhirlingCleave(level);
        }
        else
        {
            Debug.Log("해당 스킬이 없다.");
        }
    }

    // Strike;강타
    // 스킬 레벨 당 데미지를 준다.  
    private void Strike(int level)
    {
        Debug.Log("KnightSkill 클래스 Strike 함수 호출한다.");

        int totalDamage = 0;

        // 데미지 + 레벨당 데미지
        totalDamage = strikeSkillSO.damage + level * strikeSkillSO.increasedDamagePerLevel;

        Debug.Log($"Strike 데미지: {totalDamage}");
    }

    // Shield Strike;방패 강타
    // 스킬 레벨 당 스턴 시간을 준다.
    private void ShieldStrike(int level)
    {
        Debug.Log("KnightSkill 클래스 ShieldStrike 함수 호출한다.");

        int totalDamage = 0;
        float totalStunTime = 0;

        // 데미지 + 레벨당 데미지
        totalDamage = shieldStrikeSO.damage + level * shieldStrikeSO.increasedDamagePerLevel;

        // 스턴 시간 + 레벨당 스턴 시간
        totalStunTime = shieldStrikeSO.stunTime + level * shieldStrikeSO.increasedStunTimePerLevel;

        Debug.Log($"ShieldStrike 데미지: {totalDamage}");
        Debug.Log($"ShieldStrike 스턴 시간: {totalStunTime}");
    }

    // Whirling Cleave;회전 베기
    // 스킬 레벨 당 데미지를 준다.
    private void WhirlingCleave(int level)
    {
        Debug.Log("KnightSkill 클래스 WhirlingCleave 함수 호출한다.");

        int totalDamage = 0;

        // 데미지 + 레벨당 데미지
        totalDamage = whirlingCleaveSO.damage + level * whirlingCleaveSO.increasedDamagePerLevel;

        Debug.Log($"WhirlingCleave 데미지: {totalDamage}");
    }
}

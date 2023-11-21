using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    public Dictionary<string, Dictionary<int, float>> activeSkillDataDict;

    string strikeSkillName = "Strike";
    string shieldStrikeSkillName = "Shield Strike";
    string whirlingCleaveSkillName = "Whirling Cleave";

    private void Start()
    {
        SkillDataParser parser = GetComponent<SkillDataParser>();
        activeSkillDataDict = parser.ParseSkillData();

        Use(strikeSkillName, 1);
        Use(shieldStrikeSkillName, 1);
        Use(whirlingCleaveSkillName, 3);
    }

    public void Use(string name, int level)
    {
        if (name == strikeSkillName)
        {
            Strike(level);
        }
        else if (name == shieldStrikeSkillName)
        {
            ShieldStrike(level);
        }
        else if (name == whirlingCleaveSkillName)
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
        Dictionary<int, float> levelDamageDict = activeSkillDataDict[strikeSkillName];

        if (levelDamageDict.ContainsKey(level))
        {
            float damageAtLevel = levelDamageDict[level];
            UnityEngine.Debug.Log($"스킬을 사용했다. 이름:{strikeSkillName}, 레벨:{level}, 데미지:{damageAtLevel}");
            // 여기에서 damageAtLevel을 이용하여 스킬 효과를 적용할 수 있다.
        }
        else
        {
            UnityEngine.Debug.LogError($"해당 레벨의 데미지가 없다. 이름:{strikeSkillName}, 레벨:{level}");
        }
    }

    // Shield Strike;방패 강타
    // 스킬 레벨 당 스턴 시간을 준다.
    private void ShieldStrike(int level)
    {
        Dictionary<int, float> levelDamageDict = activeSkillDataDict[shieldStrikeSkillName];

        if (levelDamageDict.ContainsKey(level))
        {
            float stunAtLevel = levelDamageDict[level];
            UnityEngine.Debug.Log($"스킬을 사용했다. 이름:{strikeSkillName}, 레벨:{level}, 스턴 시간:{stunAtLevel}");
            // 여기에서 stunAtLevel 이용하여 스킬 효과를 적용할 수 있다.
        }
        else
        {
            UnityEngine.Debug.LogError($"해당 레벨의 스턴 시간이 없다. 이름:{strikeSkillName}, 레벨:{level}");
        }
    }

    // Whirling Cleave;회전 베기
    // 스킬 레벨 당 데미지를 준다.
    private void WhirlingCleave(int level)
    {
        Dictionary<int, float> levelDamageDict = activeSkillDataDict[whirlingCleaveSkillName];

        if (levelDamageDict.ContainsKey(level))
        {
            float damageAtLevel = levelDamageDict[level];
            UnityEngine.Debug.Log($"스킬을 사용했다. 이름:{strikeSkillName}, 레벨:{level}, 데미지:{damageAtLevel}");
            // 여기에서 damageAtLevel을 이용하여 스킬 효과를 적용할 수 있다.
        }
        else
        {
            UnityEngine.Debug.LogError($"해당 레벨의 데미지가 없다. 이름:{strikeSkillName}, 레벨:{level}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Skill")]
public class SkillSO : ScriptableObject
{
    [field: SerializeField] public string skillName;
    //[field: SerializeField] public Sprite icon;
    [field: SerializeField] public string description;
    [field: SerializeField] public int damage;
    [field: SerializeField] public int increasedDamagePerLevel;
    [field: SerializeField] public float stunTime;
    [field: SerializeField] public float increasedStunTimePerLevel;
    [field: SerializeField] public int manaCost;
    [field: SerializeField] public float coolTime;
   
}

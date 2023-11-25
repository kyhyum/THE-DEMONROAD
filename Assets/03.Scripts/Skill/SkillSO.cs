using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Skill")]
public class SkillSO : ScriptableObject
{
    [field: SerializeField] public string skillName;
    //[field: SerializeField] public Sprite icon;
    [field: SerializeField] public string description;
    [field: SerializeField] public float ManaCost;
}

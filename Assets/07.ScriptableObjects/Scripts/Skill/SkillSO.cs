using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    [field: SerializeField] public string skillName;
    [field: SerializeField] public string description;
    [field: SerializeField] public Sprite icon;
    [field: SerializeField] public SkillType type;
    [field: SerializeField] public int manaCost;
    [field: SerializeField] public float cooltime;
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackSkillSO : ScriptableObject
{
    [field: SerializeField] public string effect { get; private set; }
    [field: SerializeField] public GameObject range { get; private set; }
}
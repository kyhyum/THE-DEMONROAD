using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSO : ScriptableObject
{
    [field: SerializeField] public string skilName { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    // [field: SerializeField] public Jop jop { get; private set; }
    [field: SerializeField] public Texture2D texture { get; private set; }

}

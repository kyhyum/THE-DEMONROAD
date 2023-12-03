using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public TMP_Text skillPoint;
    public SkillSlot[] slots;

    public void Init()
    {
        UpdateSkillPoint(GameManager.Instance.data.skillPoint);
        GameManager.Instance.condition.OnSkillPointChanged += UpdateSkillPoint;
    }

    public void Set(Skill[] skills)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Set(skills[i]);
        }
    }

    private void UpdateSkillPoint(int sp)
    {
        skillPoint.text = sp.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public SkillSlot[] slots;

    public void Set(Skill[] skills)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Set(skills[i]);
        }
    }


}

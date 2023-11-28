using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public SkillSlot[] slots;

    public void ClickUI()
    {
        UIManager.Instance.ClickSkillUI();
    }
}

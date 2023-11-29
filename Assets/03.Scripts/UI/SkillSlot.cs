using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    private Skill skill;
    public Image icon;
    public TMP_Text nameTxt;
    public TMP_Text descriptionTxt;
    public TMP_Text levelTxt;
    public int slotID;

    public Button addBtn;
    public Button subBtn;

    public void Set(Skill skill)
    {
        this.skill = skill;

        nameTxt.text = skill.skillName;
        descriptionTxt.text = skill.description;
        levelTxt.text = string.Format("{0} / 5", skill.level);

        if (skill.level == 0)
        {
            subBtn.interactable = false;
        }

        if (skill.level == 5)
        {
            addBtn.interactable = false;
        }
    }

    public Skill GetSkill()
    {
        return skill;
    }

    public void ClickAddBtn()
    {
        subBtn.interactable = true;

        skill.LevelUp();

        if (skill.level == 5)
        {
            addBtn.interactable = false;
        }
    }

    public void ClickSubBtn()
    {
        addBtn.interactable = true;

        skill.LevelDown();

        if (skill.level == 0)
        {
            subBtn.interactable = false;
        }
    }
}

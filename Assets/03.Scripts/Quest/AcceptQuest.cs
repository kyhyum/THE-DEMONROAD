using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AcceptQuest : MonoBehaviour
{
    public TMP_Text questName;
    public QuestSO questSO;
    public void OnQuestObjectClick()
    {
        UIManager.Instance.GetQuestLog().ShowLogQuestDetails(questSO);
    }
}



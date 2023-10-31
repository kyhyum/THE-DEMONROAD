using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestAcceptWindow : MonoBehaviour
{
    public QuestSO quests;
    public GameObject acceptWindow;
    public TMP_Text questTitle;
    public TMP_Text questCondition;
    public TMP_Text questReward;

    public void InitializeAcceptWindow()
    {
        questTitle.text = quests.questName;
        questCondition.text = quests.questCondition;
        questReward.text = quests.questReward;
    }

    public void CloseWindow()
    {
        acceptWindow.SetActive(false);
    }
}

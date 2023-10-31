
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour
{
    public GameObject questListPanel;
    public GameObject questDetailsPanel;
    public GameObject questButton;
    public TMP_Text questTitleText;
    public TMP_Text questacceptNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text questConditionText;
    public TMP_Text questRewardText;
    public List<QuestSO> quests;

    public void Start()
    {
        InitializeQuestList();
    }

    private void InitializeQuestList()
    {
        foreach(QuestSO quest in quests)
        {
            if (questButton != null)
            {
                TMP_Text buttonText = questButton.GetComponentInChildren<TMP_Text>();
                buttonText.text = quest.questName;

                
                questButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ShowQuestDetails(quest);
                });
            }
            else
            {
                Debug.LogError("Existing quest button not found!");
            }
        }
    }
    private void ShowQuestDetails(QuestSO selectedQuest)
    {
        questDetailsPanel.SetActive(true);
        questacceptNameText.text = selectedQuest.questName;
        questTitleText.text = selectedQuest.questName;
        questDescriptionText.text = selectedQuest.questDescription;
        questConditionText.text = selectedQuest.questCondition;
        questRewardText.text = selectedQuest.questReward;
    }
}

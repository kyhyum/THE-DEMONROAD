using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour
{
    public GameObject questListPanel;   
    public GameObject acceptPopup;
    public GameObject cancelPopup;
    public GameObject questButton;
    public GameObject questLogPanel;
    public QuestType questType;

    //quest board
    public TMP_Text questTitleText;
    public TMP_Text questDescriptionText;
    public TMP_Text questConditionText;
    public TMP_Text questRewardText;
    
    //questlog
    public TMP_Text questLogName;
    public TMP_Text questLogSelected;
    public TMP_Text questLogDescription;
    public TMP_Text questLogRewards;

    public List<QuestSO> quests;
    public List<QuestSO> acceptedQuests = new List<QuestSO>();

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
        AcceptQuest(selectedQuest);

        
        questTitleText.text = selectedQuest.questName;
        questDescriptionText.text = selectedQuest.questDescription;
        questConditionText.text = selectedQuest.questCondition;
        questRewardText.text = selectedQuest.questReward;
    }
    private bool IsQuestAlreadyAccepted(QuestSO quest)
    {
        foreach (var acceptedQuest in acceptedQuests)
        {
            if (acceptedQuest.questType == quest.questType) 
            {
                return true;
            }
        }
        return false;
    }
    private void AcceptQuest(QuestSO quest)
    {
        if (!IsQuestAlreadyAccepted(quest))
        {
            acceptedQuests.Add(quest);
            acceptPopup.SetActive(true);
            UpdateQuestLogUI();
        }
        else
        {
            Debug.Log("Quest already accepted!");
            cancelPopup.SetActive(true);
        }
    }
    private void UpdateQuestLogUI()
    {
        questLogName.text = "";
        questLogSelected.text = "";
        questLogDescription.text = "";
        questLogRewards.text = "";

        foreach (var acceptedQuest in acceptedQuests)
        {
            questLogName.text = acceptedQuest.questName;
            questLogSelected.text = acceptedQuest.questName;
            questLogDescription.text = acceptedQuest.questDescription;
            questLogRewards.text = acceptedQuest.questReward;
        }
    }

    public void OnQuestObjectClick(QuestSO quest)
    {
        ShowLogQuestDetails(quest);
    }  

    private void ShowLogQuestDetails(QuestSO selectedQuest)
    {
        
        questLogSelected.text = selectedQuest.questName;
        questLogDescription.text = selectedQuest.questDescription;
        questLogRewards.text = selectedQuest.questReward;
    }




}

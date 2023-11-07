using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour
{
    //gameobject
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

    //quest progress
    public TMP_Text questProgName;

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
    private void ShowQuestDetails(QuestSO selectedQuest) //questBoard에서 표시되는 퀘스트 정보
    {
        AcceptQuest(selectedQuest);

        questTitleText.text = selectedQuest.questName;
        questDescriptionText.text = selectedQuest.questDescription;
        questConditionText.text = selectedQuest.questCondition;
        questRewardText.text = selectedQuest.questReward;
    }
    private bool IsQuestAlreadyAccepted(QuestSO quest) // 같은 타입의 퀘스트는 한번만 받게끔
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
    private void AcceptQuest(QuestSO quest) // questBoard에서 퀘스트를 수락
    {
        if (!IsQuestAlreadyAccepted(quest))
        {
            acceptedQuests.Add(quest);
            acceptPopup.SetActive(true);
            UpdateQuestLogUI();
            ShowQuestProgress(quest);

            foreach (var npc in quest.relatedNPCs)
            {
                if (npc != null)
                {
                    npc.hasQuest = true;
                    npc.conversationCount = 1;
                }
                
            }

        }
        else
        {
            Debug.Log("Quest already accepted!");
            cancelPopup.SetActive(true);
        }
    }

   

    private void UpdateQuestLogUI() //questLog에 선택된 퀘스트 정보 표시
    {
        questLogName.text = "";
        questLogSelected.text = "";
        questLogDescription.text = "";
        questLogRewards.text = "";

        foreach (var acceptedQuest in acceptedQuests)
        {
            questLogName.text += acceptedQuest.questName;
            questLogSelected.text += acceptedQuest.questName;
            questLogDescription.text += acceptedQuest.questDescription;
            questLogRewards.text += acceptedQuest.questReward;
        }
        
    }

    public void OnQuestObjectClick(QuestSO quest) //questLog에서 퀘스트를 하나하나 선택
    {
        ShowLogQuestDetails(quest);
        
    }  

    private void ShowLogQuestDetails(QuestSO selectedQuest) // questLog에서 선택된 퀘스트 정보 표시
    {
        
        questLogSelected.text = selectedQuest.questName;
        questLogDescription.text = selectedQuest.questDescription;
        questLogRewards.text = selectedQuest.questReward;
    }

    public void ShowQuestProgress(QuestSO selectedQuest) //questProgress 표시창
    {
        if (selectedQuest.questType == QuestType.ConversationQuest) //대화퀘스트
        {
            foreach (var npc in selectedQuest.relatedNPCs)
            {    
                questProgName.text = selectedQuest.questName + "\n - " + npc.conversationCount + " / " + selectedQuest.questComplete;    

            }
        }
        else if(selectedQuest.questType == QuestType.ItemQuest) //아이템퀘스트
        {
            questProgName.text = selectedQuest.questName + "\n - " + "현재상황 / " + selectedQuest.questComplete;
        }
        else if (selectedQuest.questType == QuestType.MonsterQuest) //몬스터퀘스트
        {
            questProgName.text = selectedQuest.questName + "\n - " + "현재상황 / " + selectedQuest.questComplete;
        } 
    }

    public void RemoveAcceptedQuest(QuestSO selectedquest) // Remove가 현재 안되는중
    {
        acceptedQuests.Remove(selectedquest);
        Debug.Log("퀘스트 삭제");
        UpdateQuestLogUI();
    }
}

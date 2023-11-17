using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour
{
    //gameobject
       
    public GameObject acceptPopup;
    public GameObject cancelPopup;
    public GameObject questLogPanel;
    [SerializeField] Button[] questButton;

    //quest board
    [SerializeField] TMP_Text[] questTitleText;
    public TMP_Text questDescriptionText;
    public TMP_Text questConditionText;
    public TMP_Text questRewardText;
    [SerializeField] Button acceptButton;
    
    //questlog
    [SerializeField] TMP_Text[] questLogName;
    public TMP_Text questLogSelected;
    public TMP_Text questLogDescription;
    public TMP_Text questLogRewards;

    //quest progress
    [SerializeField] TMP_Text questProgmonsterName;
    [SerializeField] TMP_Text questProgitemName;
    [SerializeField] TMP_Text questProgconverseName;
    [SerializeField] TMP_Text questProgInfinitemonsterName;

    [SerializeField] List<QuestSO> quests;
    public List<QuestSO> Quests { get { return quests; } }

    QuestSO selectQuest;
    PlayerData player;

    public void Start()
    {
        player = GameManager.Instance.player;
        InitializeQuestList();
        acceptButton.onClick.AddListener(() => { AcceptQuest(selectQuest); });
    }

    private void InitializeQuestList()
    {
        for(int i = 0; i < quests.Count; i++)
        {
            int index = i;
            questTitleText[i].text = quests[i].questName;
            questButton[i].onClick.AddListener(() => { ShowQuestDetails(quests[index]); });
        }
    }
    private void ShowQuestDetails(QuestSO selectedQuest) //questBoard에서 표시되는 퀘스트 정보
    {
        questDescriptionText.text = selectedQuest.questDescription;
        questConditionText.text = selectedQuest.questCondition;
        questRewardText.text = selectedQuest.questReward;
        selectQuest = selectedQuest;
    }
    private bool IsQuestAlreadyAccepted(QuestSO quest) // 같은 타입의 퀘스트는 한번만 받게끔
    {
        if (player.acceptQuest.Contains(quest))
        {
            return true;
        }
        return false;
    }
    private void AcceptQuest(QuestSO quest) // questBoard에서 퀘스트를 수락
    {
        if (!IsQuestAlreadyAccepted(quest))
        {
            player.acceptQuest.Add(quest);
            acceptPopup.SetActive(true);
            UpdateQuestLogUI();
            ShowQuestProgress(quest);

            foreach (var npc in quest.relatedNPCs)
            {
                if (npc != null)
                {
                    npc.hasQuest = true;
                    
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
        //questLogName.text = "";
        for (int i = 0; i < questLogName.Length; i++)
        {
            questLogName[i].text = "";
        }
        questLogSelected.text = "";
        questLogDescription.text = "";
        questLogRewards.text = "";

        foreach (var acceptedQuest in player.acceptQuest)
        {
            
            int questIndex = acceptedQuest.questIndex;

            
            if (questIndex >= 0 && questIndex < questLogName.Length)
            {
                
                questLogName[questIndex].text = acceptedQuest.questName;
            }

            
            
        }

        
        questLogSelected.text = "";
        questLogDescription.text = "";
        questLogRewards.text = "";
        

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

                questProgconverseName.text = selectedQuest.questName + "\n - " + npc.conversationCount + " / " + selectedQuest.questComplete;

            }
        }
        if (selectedQuest.questType == QuestType.ItemQuest) //아이템퀘스트
        {
            questProgitemName.text = selectedQuest.questName + "\n - " + "현재상황 / " + selectedQuest.questComplete;
        }
        else if (selectedQuest.questType == QuestType.MonsterQuest) //몬스터퀘스트
        {
            questProgmonsterName.text = selectedQuest.questName + "\n - " + "현재상황 / " + selectedQuest.questComplete;
        }
        else if (selectedQuest.questType == QuestType.InfiniteMonsterQuest) //무한몬스터퀘스트
        {
            questProgInfinitemonsterName.text = selectedQuest.questName + "\n - " + "현재상황 / " + selectedQuest.questComplete;
        }
    }
   




}

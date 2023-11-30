using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour
{
    //gameobject 
    public GameObject acceptPopup;
    public GameObject cancelPopup;
    public GameObject questacceptCheckPop;
    [SerializeField] Button[] questButton;

    //quest board
    [SerializeField] TMP_Text[] questTitleText;
    public TMP_Text questDescriptionText;
    public TMP_Text questConditionText;
    public TMP_Text questRewardText;
    [SerializeField] Button acceptButton;
    
    [SerializeField] List<QuestSO> quests;
    public static UIManager Instance;

    //메인퀘스트 관련
    private ChoiceDungeon choiceDungeon;
    

    public List<QuestSO> Quests { get { return quests; } }
    public ItemSO golditem;
    public QuestProgress questProgress;
    public QuestLog questLog;
    QuestSO selectQuest;
    PlayerData player;

    public void Start()
    {
        player = GameManager.Instance.data;
        questacceptCheckPop.SetActive(false);
        
        if(questLog == null )
        {
            Debug.LogError("QuestLog 컴포넌트를 찾을 수 없습니다.");
            return;
        }
        if(questProgress == null)
        {
            Debug.LogError("QuestProgress 컴포넌트를 찾을 수 없습니다.");
        }

        
        choiceDungeon = FindObjectOfType<ChoiceDungeon>();
        
        if (choiceDungeon != null)
        {
            ChoiceDungeon.DungeonInteractionPopupActivated += OnDungeonInteractionPopupActivated;
        }

        InitializeQuestList();
        acceptButton.onClick.AddListener(AcceptButtonClicked);
    }
    private void AcceptButtonClicked()
    {
        if (selectQuest == null)
        {
            Debug.Log("퀘스트를 선택해주세요.");
            questacceptCheckPop.SetActive(true);
            return;
        }

        AcceptQuest(selectQuest);
    }
    private void OnDungeonInteractionPopupActivated()
    {
        QuestSO selectedQuest = GetMainQuest();
        questProgress.UpdateMainQuestProgress(selectedQuest);
    }

    private QuestSO GetMainQuest()
    {
        QuestSO mainQuest = quests[4];
        return mainQuest;
    }

    private void InitializeQuestList()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            int index = i;
            questTitleText[i].text = quests[i].questName;
            questButton[i].onClick.AddListener(() => { ShowQuestDetails(quests[index]); });
        }
    }
    private void ShowQuestDetails(QuestSO selectedQuest) 
    {
        questDescriptionText.text = selectedQuest.questDescription;
        questConditionText.text = selectedQuest.questCondition;
        questRewardText.text = selectedQuest.questReward;
        selectQuest = selectedQuest;
    }

    
    private bool IsQuestAlreadyAccepted(QuestSO quest)
    {
        if (player.acceptQuest.Contains(quest))
        {
            return true;
        }
        return false;
    }
    private void AcceptQuest(QuestSO quest) 
    {
        if (!IsQuestAlreadyAccepted(quest))
        {
            player.acceptQuest.Add(quest);
            acceptPopup.SetActive(true);
            questLog.UpdateQuestLogUI();
            questProgress.ShowQuestProgress(quest);

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

    void OnDestroy()
    {
        
        if (choiceDungeon != null)
        {
            ChoiceDungeon.DungeonInteractionPopupActivated -= OnDungeonInteractionPopupActivated;
        }
    }




}

using System.Collections.Generic;
using TMPro;
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
    public TMP_Text questProgmainName;

    [SerializeField] List<QuestSO> quests;
    public static UIManager Instance;

    private QuestController controller;
    //int goblinKills = DungeonManager.Instance.goblinkillCount;

    //메인퀘스트 관련
    private ChoiceDungeon choiceDungeon;
    private bool isMainQuestProgressUpdated = false;

    public List<QuestSO> Quests { get { return quests; } }
    public ItemSO golditem;
    QuestSO selectQuest;
    PlayerData player;

    public void Start()
    {
        player = GameManager.Instance.player;
        

        controller = FindAnyObjectByType<QuestController>();
        choiceDungeon = FindObjectOfType<ChoiceDungeon>();
        //이벤트 구독
        if (choiceDungeon != null)
        {
            ChoiceDungeon.DungeonInteractionPopupActivated += OnDungeonInteractionPopupActivated;
        }

        InitializeQuestList();
        acceptButton.onClick.AddListener(() => { AcceptQuest(selectQuest); });
    }
    private void OnDungeonInteractionPopupActivated()
    {
        QuestSO selectedQuest = GetMainQuest();
        UpdateMainQuestProgress(selectedQuest );
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
    private void ShowQuestDetails(QuestSO selectedQuest) //questBoard에서 표시되는 퀘스트 정보
    {
        questDescriptionText.text = selectedQuest.questDescription;
        questConditionText.text = selectedQuest.questCondition;
        questRewardText.text = selectedQuest.questReward;
        selectQuest = selectedQuest;
    }

    // 같은 타입의 퀘스트는 한번만 받게끔
    private bool IsQuestAlreadyAccepted(QuestSO quest) 
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
        else if (selectedQuest.questType == QuestType.ItemQuest) //아이템퀘스트 = TODO:드롭되는 아이템 갯수 카운트 해서 '0'에 반영
        {
            questProgitemName.text = selectedQuest.questName + "\n - " + "0 / " + selectedQuest.questComplete;
        }
        else if (selectedQuest.questType == QuestType.MonsterQuest) //몬스터퀘스트 
        {
            //int goblinKills = DungeonManager.Instance.goblinkillCount;
            int goblinKills = 0;
            questProgmonsterName.text = selectedQuest.questName + "\n - "  +goblinKills+ " / " + selectedQuest.questComplete;

            if (goblinKills >= selectedQuest.questComplete)
            {
                questProgmonsterName.color = Color.red;
                questProgmonsterName.fontStyle |= FontStyles.Italic;
                questProgmonsterName.fontStyle |= FontStyles.Strikethrough;
                MonsterQuestReward(selectedQuest);
            }
        }
        else if (selectedQuest.questType == QuestType.InfiniteMonsterQuest) //무한몬스터퀘스트
        {
            int goblinKills = 0;
            //int goblinKills = DungeonManager.Instance.goblinkillCount;
            questProgInfinitemonsterName.text = selectedQuest.questName + "\n - "  +goblinKills+ " / " + selectedQuest.questComplete;
            if (goblinKills >= selectedQuest.questComplete)
            {
                questProgInfinitemonsterName.color = Color.red;
                questProgInfinitemonsterName.fontStyle |= FontStyles.Italic;
                questProgInfinitemonsterName.fontStyle |= FontStyles.Strikethrough;
                InfiniteMonsterQuestReward(selectedQuest);
                //여기에 새로운 퀘스트 추가 - 150마리 잡는 퀘스트.. 시간되면

            }
        }
        else if (selectedQuest.questType == QuestType.MainQuest) //메인퀘스트 
        {
            questProgmainName.text = selectedQuest.questName + "\n - " + "0 / " + selectedQuest.questComplete;
            //UpdateMainQuestProgress(selectedQuest);


        }
    }
    public void UpdateMainQuestProgress(QuestSO selectedQuest)
    {
        if (!isMainQuestProgressUpdated)
        {
            if (choiceDungeon != null && choiceDungeon.IsDungeonInteractionPopupActive())
            {
                Debug.Log("UpdateMainQuest이 null이 아니다");

                
                questProgmainName.color = Color.red;
                questProgmainName.fontStyle |= FontStyles.Italic;
                questProgmainName.fontStyle |= FontStyles.Strikethrough;

                questProgmainName.text = selectedQuest.questName + "\n - " + "1 / " + selectedQuest.questComplete;     

                // 보상처리
                MainQuestReward(selectedQuest);
                isMainQuestProgressUpdated = true;


            }
            

        }

    }

    void OnDestroy()
    {
        // 이벤트 구독 해제
        if (choiceDungeon != null)
        {
            ChoiceDungeon.DungeonInteractionPopupActivated -= OnDungeonInteractionPopupActivated;
        }
    }




    public void MainQuestReward(QuestSO selectedQuest)
    {
        if (controller != null)
        {
            controller.ShowPopup();
            controller.Invoke("HidePopup", 2f);

        }
        else if (controller == null)
        {
            Debug.Log("Null입니다");
        }

        if (selectQuest != null && selectQuest.questType == QuestType.MainQuest)
        {
            Inventory inventory = UIManager.Instance.GetInventory();

            if (inventory != null)
            {
                ItemSO goldItem = Resources.Load<ItemSO>("Gold");
                Item itemToAdd = new Item(goldItem);
                // 금화 아이템을 Inventory에 추가하는 로직
                if (inventory.AddItem(itemToAdd))
                {
                    inventory.Gold += selectedQuest.questRewardCoin;
                    Debug.Log("보상으로 " + selectedQuest.questRewardCoin + "개의 금화 획득!");
                }
                else
                {
                    Debug.Log("아이템 추가에 실패했습니다.");
                }
            }
            else
            {
                Debug.Log("Inventory가 null입니다.");
            }
        }
    }
    

    public void MonsterQuestReward(QuestSO selectedQuest)
    {
        if (controller != null)
        {
            controller.ShowPopup();
            controller.Invoke("HidePopup", 2f);

        }
        else if (controller == null)
        {
            Debug.Log("Null입니다");
        }

        if (selectQuest != null && selectQuest.questType == QuestType.MonsterQuest)
        {
            Inventory inventory = UIManager.Instance.GetInventory();

            if (inventory != null)
            {
                inventory.Gold += selectQuest.questRewardCoin;
                Debug.Log("보상으로 " + selectQuest.questRewardCoin + "개의 금화 획득!");
            }
            else
            {
                Debug.Log("Inventory가 null입니다.");
            }
        }
    }
    public void InfiniteMonsterQuestReward(QuestSO selectedQuest)
    {
        if (controller != null)
        {
            controller.ShowPopup();
            controller.Invoke("HidePopup", 2f);

        }
        else if (controller == null)
        {
            Debug.Log("Null입니다");
        }

        if (selectQuest != null && selectQuest.questType == QuestType.InfiniteMonsterQuest)
        {
            Inventory inventory = UIManager.Instance.GetInventory();

            if (inventory != null)
            {
                inventory.Gold += selectQuest.questRewardCoin;
                Debug.Log("보상으로 " + selectQuest.questRewardCoin + "개의 금화 획득!");
            }
            else
            {
                Debug.Log("Inventory가 null입니다.");
            }
        }
    }



}

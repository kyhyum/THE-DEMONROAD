using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class QuestProgress : MonoBehaviour
{
    [SerializeField] TMP_Text questProgmonsterName;
    [SerializeField] TMP_Text questProgitemName;
    public TMP_Text questProgconverseName;
    [SerializeField] TMP_Text questProgInfinitemonsterName;

    public TMP_Text questProgmainName;

    public QuestBoard board;
    public QuestController controller;
    
    void OnEnable()
    {
        
        GameManager.OnGoblinKillCountChanged += UpdateGoblinKillCountUI;
    }

    void OnDisable()
    {
        
        GameManager.OnGoblinKillCountChanged -= UpdateGoblinKillCountUI;
    }
    void UpdateGoblinKillCountUI(int newGoblinKillCount)
    {
        Debug.Log("UI 업데이트");
        int goblinKills = newGoblinKillCount;
        QuestSO quest = board.selectQuest;
        if(quest.questType == Define.QuestType.MonsterQuest)
        {
            questProgmonsterName.text = quest.questName + "\n - " + goblinKills + " / " + quest.questComplete;


            if (goblinKills >= quest.questComplete)
            {
                questProgmonsterName.color = Color.red;
                questProgmonsterName.fontStyle |= FontStyles.Italic;
                questProgmonsterName.fontStyle |= FontStyles.Strikethrough;

                MonsterQuestReward(quest);
            }
        }
        //else if (quest.questType == Define.QuestType.InfiniteMonsterQuest)
        //{
        //    questProgInfinitemonsterName.text = quest.questName + "\n - " + goblinKills + " / " + quest.questComplete;

        //    if (goblinKills >= quest.questComplete)
        //    {
        //        questProgInfinitemonsterName.color = Color.red;
        //        questProgInfinitemonsterName.fontStyle |= FontStyles.Italic;
        //        questProgInfinitemonsterName.fontStyle |= FontStyles.Strikethrough;
        //    }
        //}


    }
    public void ShowQuestProgress(QuestSO selectedquest) 
    {
        board.selectQuest = selectedquest;

        if (selectedquest.questType == Define.QuestType.ConversationQuest) 
        {
            foreach (var npc in selectedquest.relatedNPCs)
            {

                questProgconverseName.text = selectedquest.questName + "\n - " + npc.conversationCount + " / " + selectedquest.questComplete;

            }
        }
        else if (selectedquest.questType == Define.QuestType.ItemQuest) 
        {
            questProgitemName.text = selectedquest.questName + "\n - " + "0 / " + selectedquest.questComplete;
        }
        else if (selectedquest.questType == Define.QuestType.MonsterQuest)  
        {
            int goblinKills = GameManager.Instance.goblinkillCount;

            questProgmonsterName.text = selectedquest.questName + "\n - " + goblinKills + " / " + selectedquest.questComplete;
            

        }
        else if (selectedquest.questType == Define.QuestType.InfiniteMonsterQuest) 
        {
            int goblinKills = GameManager.Instance.goblinkillCount;
            questProgInfinitemonsterName.text = selectedquest.questName + "\n - " + goblinKills + " / " + selectedquest.questComplete;
            if (goblinKills >= selectedquest.questComplete)
            {
                InfiniteMonsterQuestReward(selectedquest);

            }
        }
        else if (selectedquest.questType == Define.QuestType.MainQuest)  
        {

            questProgmainName.text = selectedquest.questName + "\n - " + "0 / " + selectedquest.questComplete;
            board.UpdateMainQuestProgress(board.selectQuest);

            


        }
    }
    public void CompletePopup()
    {
        Debug.Log("CompletePop 불림");
        if (controller != null)
        {
            controller.ShowPopup();
            controller.Invoke("HidePopup", 2f);

        }
        else if (controller == null)
        {
            Debug.Log("Null입니다");
        }
    }


    public void MainQuestReward(QuestSO selectedQuest)
    {
        Debug.Log("mainquest리워드를 받습니다");

        CompletePopup();
        if (selectedQuest != null)
        {
            if (selectedQuest.questType == Define.QuestType.MainQuest)
            {
                Inventory inventory = UIManager.Instance.GetInventory();

                if (inventory != null)
                {
                    inventory.Gold += selectedQuest.questRewardCoin;
                    Debug.Log(selectedQuest.questName + "보상으로 " + selectedQuest.questRewardCoin + "개의 금화를 획득했습니다!");

                }
                else
                {
                    Debug.Log("Inventory가 null입니다.");
                }
            }
            else
            {
                Debug.Log("MainQuest가 아닙니다.");
            }
        }
        else
        {
            Debug.Log("selectQuest가 null입니다.");
        }


    }
    public void MonsterQuestReward(QuestSO selectedQuest)
    {
        Debug.Log("MonsterQuestreward를 받는다");
        CompletePopup();

        if (selectedQuest != null)
        {
            if (selectedQuest.questType == Define.QuestType.MonsterQuest)
            {
                Inventory inventory = UIManager.Instance.GetInventory();

                if (inventory != null)
                {
                    inventory.Gold += selectedQuest.questRewardCoin;
                    Debug.Log(selectedQuest.questName + "보상으로 " + selectedQuest.questRewardCoin + "개의 금화를 획득했습니다!");
                }
                else
                {
                    Debug.Log("Inventory가 null입니다.");
                }
            }
            else
            {
                Debug.Log("MonsterQuest가 아닙니다.");
            }
        }
        else
        {
            Debug.Log("selectQuest가 null입니다.");
        }
    }
    public void InfiniteMonsterQuestReward(QuestSO selectedQuest)
    {
        CompletePopup();

        if (selectedQuest != null)
        {
            if (selectedQuest.questType == Define.QuestType.InfiniteMonsterQuest)
            {
                Inventory inventory = UIManager.Instance.GetInventory();

                if (inventory != null)
                {
                    inventory.Gold += selectedQuest.questRewardCoin;
                    Debug.Log(selectedQuest.questName + "보상으로 " + selectedQuest.questRewardCoin + "개의 금화를 획득했습니다!");
                }
                else
                {
                    Debug.Log("Inventory가 null입니다.");
                }
            }
            else
            {
                Debug.Log("MainQuest가 아닙니다.");
            }
        }
        else
        {
            Debug.Log("selectQuest가 null입니다.");
        }
    }
}

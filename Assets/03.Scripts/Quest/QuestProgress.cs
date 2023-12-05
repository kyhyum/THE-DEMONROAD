using System.Collections.Generic;
using System.Text;
using TMPro;
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
    private QuestController controller;
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
        
        
        questProgmonsterName.text = board.selectQuest.questName + "\n - " + goblinKills + " / " + board.selectQuest.questComplete;

        
        if (goblinKills >= board.selectQuest.questComplete)
        {
            questProgmonsterName.color = Color.red;
            questProgmonsterName.fontStyle |= FontStyles.Italic;
            questProgmonsterName.fontStyle |= FontStyles.Strikethrough;
            
        }
    }
    public void ShowQuestProgress() 
    {
        if (board.selectQuest.questType == Define.QuestType.ConversationQuest) 
        {
            foreach (var npc in board.selectQuest.relatedNPCs)
            {

                questProgconverseName.text = board.selectQuest.questName + "\n - " + npc.conversationCount + " / " + board.selectQuest.questComplete;

            }
        }
        else if (board.selectQuest.questType == Define.QuestType.ItemQuest) 
        {
            questProgitemName.text = board.selectQuest.questName + "\n - " + "0 / " + board.selectQuest.questComplete;
        }
        else if (board.selectQuest.questType == Define.QuestType.MonsterQuest)  
        {
            int goblinKills = GameManager.Instance.goblinkillCount;

            questProgmonsterName.text = board.selectQuest.questName + "\n - " + goblinKills + " / " + board.selectQuest.questComplete;
            if (goblinKills >= board.selectQuest.questComplete)
            {
                MonsterQuestReward();

            }

        }
        else if (board.selectQuest.questType == Define.QuestType.InfiniteMonsterQuest) 
        {
            int goblinKills = GameManager.Instance.goblinkillCount;
            questProgInfinitemonsterName.text = board.selectQuest.questName + "\n - " + goblinKills + " / " + board.selectQuest.questComplete;
            if (goblinKills >= board.selectQuest.questComplete)
            {
                InfiniteMonsterQuestReward();

            }
        }
        else if (board.selectQuest.questType == Define.QuestType.MainQuest)  
        {

            questProgmainName.text = board.selectQuest.questName + "\n - " + "0 / " + board.selectQuest.questComplete;
            board.UpdateMainQuestProgress(board.selectQuest);

            


        }
    }


    public void MainQuestReward()
    {
        Debug.Log("mainquest리워드를 받습니다");
        if (controller != null)
        {
            controller.ShowPopup();
            controller.Invoke("HidePopup", 2f);

        }
        else if (controller == null)
        {
            Debug.Log("Null입니다");
        }
        if (board.selectQuest != null)
        {
            if (board.selectQuest.questType == Define.QuestType.MainQuest)
            {
                Inventory inventory = UIManager.Instance.GetInventory();

                if (inventory != null)
                {
                    inventory.Gold += board.selectQuest.questRewardCoin;
                    Debug.Log(board.selectQuest.questName + "보상으로 " + board.selectQuest.questRewardCoin + "개의 금화를 획득했습니다!");
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
    public void MonsterQuestReward()
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

        if (board.selectQuest != null)
        {
            if (board.selectQuest.questType == Define.QuestType.MonsterQuest)
            {
                Inventory inventory = UIManager.Instance.GetInventory();

                if (inventory != null)
                {
                    inventory.Gold += board.selectQuest.questRewardCoin;
                    Debug.Log(board.selectQuest.questName + "보상으로 " + board.selectQuest.questRewardCoin + "개의 금화를 획득했습니다!");
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
    public void InfiniteMonsterQuestReward()
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

        if (board.selectQuest != null)
        {
            if (board.selectQuest.questType == Define.QuestType.InfiniteMonsterQuest)
            {
                Inventory inventory = UIManager.Instance.GetInventory();

                if (inventory != null)
                {
                    inventory.Gold += board.selectQuest.questRewardCoin;
                    Debug.Log(board.selectQuest.questName + "보상으로 " + board.selectQuest.questRewardCoin + "개의 금화를 획득했습니다!");
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

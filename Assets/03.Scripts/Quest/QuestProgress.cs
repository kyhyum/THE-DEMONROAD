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
    public void ShowQuestProgress(QuestSO selectedQuest) 
    {
        if (selectedQuest.questType == Define.QuestType.ConversationQuest) 
        {
            foreach (var npc in selectedQuest.relatedNPCs)
            {

                questProgconverseName.text = selectedQuest.questName + "\n - " + npc.conversationCount + " / " + selectedQuest.questComplete;

            }
        }
        else if (selectedQuest.questType == Define.QuestType.ItemQuest) 
        {
            questProgitemName.text = selectedQuest.questName + "\n - " + "0 / " + selectedQuest.questComplete;
        }
        else if (selectedQuest.questType == Define.QuestType.MonsterQuest)  
        {
            int goblinKills = GameManager.Instance.goblinkillCount;

            questProgmonsterName.text = selectedQuest.questName + "\n - " + goblinKills + " / " + selectedQuest.questComplete;
            if (goblinKills >= selectedQuest.questComplete)
            {
                MonsterQuestReward(selectedQuest);

            }

        }
        else if (selectedQuest.questType == Define.QuestType.InfiniteMonsterQuest) 
        {
            int goblinKills = GameManager.Instance.goblinkillCount;
            questProgInfinitemonsterName.text = selectedQuest.questName + "\n - " + goblinKills + " / " + selectedQuest.questComplete;
            if (goblinKills >= selectedQuest.questComplete)
            {
                InfiniteMonsterQuestReward(selectedQuest);

            }
        }
        else if (selectedQuest.questType == Define.QuestType.MainQuest)  
        {

            questProgmainName.text = selectedQuest.questName + "\n - " + "0 / " + selectedQuest.questComplete;
            board.UpdateMainQuestProgress(selectedQuest);

            


        }
    }


    public void MainQuestReward(QuestSO selectedQuest)
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
        if (controller != null)
        {
            controller.ShowPopup();
            controller.Invoke("HidePopup", 2f);

        }
        else if (controller == null)
        {
            Debug.Log("Null입니다");
        }

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
                Debug.Log("MainQuest가 아닙니다.");
            }
        }
        else
        {
            Debug.Log("selectQuest가 null입니다.");
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

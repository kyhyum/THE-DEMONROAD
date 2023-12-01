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
    [SerializeField] TMP_Text questProgconverseName;
    [SerializeField] TMP_Text questProgInfinitemonsterName;

    public TMP_Text questProgmainName;

    private QuestController controller;
    private ChoiceDungeon choiceDungeon;
    private bool isMainQuestProgressUpdated = false;

    

    public void ShowQuestProgress(QuestSO selectedQuest) 
    {
        if (selectedQuest.questType == Define.QuestType.ConversationQuest) 
        {
            foreach (var npc in selectedQuest.relatedNPCs)
            {

                questProgconverseName.text = selectedQuest.questName + "\n - " + npc.conversationCount + " / " + selectedQuest.questComplete;

            }
        }
        else if (selectedQuest.questType == Define.QuestType.ItemQuest) //아이템퀘스트 = TODO:드롭되는 아이템 갯수 카운트 해서 '0'에 반영
        {
            questProgitemName.text = selectedQuest.questName + "\n - " + "0 / " + selectedQuest.questComplete;
        }
        else if (selectedQuest.questType == Define.QuestType.MonsterQuest)  
        {
            int goblinKills = GameManager.Instance.goblinkillCount;

            questProgmonsterName.text = selectedQuest.questName + "\n - " + goblinKills + " / " + selectedQuest.questComplete;

            if (goblinKills >= selectedQuest.questComplete)
            {
                questProgmonsterName.color = Color.red;
                questProgmonsterName.fontStyle |= FontStyles.Italic;
                questProgmonsterName.fontStyle |= FontStyles.Strikethrough;
                MonsterQuestReward(selectedQuest);
            }
        }
        else if (selectedQuest.questType == Define.QuestType.InfiniteMonsterQuest) 
        {

            int goblinKills = GameManager.Instance.goblinkillCount;
            questProgInfinitemonsterName.text = selectedQuest.questName + "\n - " + goblinKills + " / " + selectedQuest.questComplete;
            if (goblinKills >= selectedQuest.questComplete)
            {
                questProgInfinitemonsterName.color = Color.red;
                questProgInfinitemonsterName.fontStyle |= FontStyles.Italic;
                questProgInfinitemonsterName.fontStyle |= FontStyles.Strikethrough;
                InfiniteMonsterQuestReward(selectedQuest);
                

            }
        }
        else if (selectedQuest.questType == Define.QuestType.MainQuest)  
        {

            questProgmainName.text = selectedQuest.questName + "\n - " + "0 / " + selectedQuest.questComplete;
            UpdateMainQuestProgress(selectedQuest);


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

                
                MainQuestReward(selectedQuest);
                isMainQuestProgressUpdated = true;


            }


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

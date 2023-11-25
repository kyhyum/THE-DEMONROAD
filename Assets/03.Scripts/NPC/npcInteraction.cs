using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;


public class npcInteraction : MonoBehaviour
{
    public NPCSO npc;
    public QuestSO quest;
    public ItemSO golditem;
    [SerializeField] List<ItemSO> allItems;
    private QuestController controller;
   
    public static UIManager Instance;
    
    
    public GameObject dialogueUI;
    public GameObject interactionPopup;
    

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    //progressui
    public TMP_Text questProgName;
    //public TMP_Text questComplete;

    private bool isUIVisible = false;
    private bool isTalking = false;
    Transform player;

    public float activationDistance = 5f;
       
    
    void Start()
    {
        controller = FindObjectOfType<QuestController>();
        

        dialogueUI.SetActive(false);
        player = GameManager.Instance.Myplayer.transform;
        
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance)
        {
            //상호작용 가능시 팝업
            
            interactionPopup.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                isUIVisible = !isUIVisible;
                if (isUIVisible)
                {
                    ShowDialogue();
                    
                }
                else
                {
                    HideDialogue();
                    UIManager.Instance.ActivePlayerUI(true);

                }
            }
        }
        else
        {
            HideDialogue();
            UIManager.Instance.ActivePlayerUI(true);
        }
        if (isUIVisible && Input.GetKeyDown(KeyCode.F) && !isTalking)
        {
            HideDialogue();
            UIManager.Instance.ActivePlayerUI(true);
        }

    }
    void ShowDialogue()
    {
        //player Ui off
        UIManager.Instance.ActivePlayerUI(false);

        isTalking = true;
        StopAllCoroutines(); 

        dialogueUI.SetActive(true);
        nameText.text = npc.npcName;

       

        if (npc.questType == QuestType.ConversationQuest)
        {
            if (npc.hasQuest)
            {
                string appropriateDialogue = npc.completeDialogue[0];
                StartCoroutine(DisplayDialogue(appropriateDialogue));  
                
                npc.conversationCount++;
                npc.hasQuest = false;
                Debug.Log("대화횟수 1증가");
                Debug.Log("현재 총 대화수: " + npc.conversationCount);

                ConversationQuestProgress(quest);
                
                if (npc.conversationCount == quest.questComplete) 
                {
                    
                    CompleteConversationQuest(npc);
                }


            }
            else
            {
                string appropriateDialogue = npc.npcDialogue[0];
                StartCoroutine(DisplayDialogue(appropriateDialogue));
                
            }
        }
        else
        {
            string appropriateDialogue = npc.npcDialogue[0];
            StartCoroutine(DisplayDialogue(appropriateDialogue));
        }

        

    }
    public void ConversationQuestProgress(QuestSO selectedQuest) 
    {
        if (selectedQuest.questType == QuestType.ConversationQuest) //대화퀘스트
        {
            foreach (var npc in selectedQuest.relatedNPCs)
            {

                
                questProgName.text = selectedQuest.questName + "\n - " +  " 1 / " + selectedQuest.questComplete;
            }
        }
        
    }
    void HideDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
        interactionPopup.SetActive(false);
        
    }



    private void CompleteConversationQuest(NPCSO npc)
    {
        npc.conversationCount--;

        foreach (var relatednpc in quest.relatedNPCs)
        {
            if (relatednpc != null)
            {
                relatednpc.hasQuest = false;

            }

        }


        // 퀘스트 완료 처리를 수행할 코드 작성
        Debug.Log("Quest completed!");

        questProgName.color = Color.red;
        questProgName.fontStyle |= FontStyles.Italic;
        questProgName.fontStyle |= FontStyles.Strikethrough;

        //금화 보상 처리

        //방법1
        //Inventory inventory = UIManager.Instance.GetInventory();
        //if (inventory != null)
        //{
        //    ItemSO goldItem = null; 
        //    foreach (var item in allItems)
        //    {
        //        if (item != null && item.type == ItemType.Gold && item.itemName == "금화")
        //        {
        //            goldItem = item;
        //            break;
        //        }
        //    }
        //    int goldAmount = quest.questRewardCoin;


        //    if (goldItem is IStackable)
        //    {
        //        IStackable stackableGoldItem = (IStackable)goldItem;
        //        stackableGoldItem.Add(goldAmount);
        //    }

        //}


        //방법2
        //아래코드는"IStackable을 구현하지 않은 아이템입니다." 오류생성

        //if (quest != null && quest.questType == QuestType.ConversationQuest)
        //{
        //    Inventory inventory = UIManager.Instance.GetInventory();

        //    if (inventory != null)
        //    {

        //        ItemSO itemSO = golditem;
        //        Item itemToAdd = new Item(itemSO);

        //        // 아이템이 IStackable을 구현하는지 확인
        //        if (itemToAdd is IStackable)
        //        {
        //            IStackable stackableItem = itemToAdd as IStackable;

        //            if (stackableItem != null)
        //            {
        //                bool itemAdded = inventory.AddItem(itemToAdd);

        //                if (itemAdded)
        //                {
        //                    inventory.Gold += quest.questRewardCoin;
        //                    Debug.Log("보상으로 " + quest.questRewardCoin + "개의 금화 획득!");
        //                }
        //                else
        //                {
        //                    Debug.Log("아이템 추가에 실패했습니다.");
        //                }
        //            }
        //            else
        //            {
        //                Debug.Log("IStackable로 캐스팅할 수 없는 아이템입니다.");
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("IStackable을 구현하지 않은 아이템입니다.");
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Inventory가 null입니다.");
        //    }
        //}

        //방법3
        if (quest != null && quest.questType == QuestType.ConversationQuest)
        {
            Inventory inventory = UIManager.Instance.GetInventory();

            if (inventory != null)
            {
                ItemSO goldItem = Resources.Load<ItemSO>("Gold");  

                Item itemToAdd = new Item(goldItem);

                // 금화 아이템을 Inventory에 추가하는 로직
                if (inventory.AddItem(itemToAdd))
                {
                    inventory.Gold += quest.questRewardCoin;
                    Debug.Log("보상으로 " + quest.questRewardCoin + "개의 금화 획득!");
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




    System.Collections.IEnumerator DisplayDialogue(string dialogue) 
    {
        dialogueText.text = "";
        bool isCompleteDialogue = dialogue == npc.completeDialogue[0];

        foreach (char letter in dialogue.ToCharArray()) 
        {
            dialogueText.text += letter; 
            yield return new WaitForSeconds(0.05f); 
        }
        if (isCompleteDialogue) // 퀘스트 완료 팝업 띄우고 2초뒤에 닫기
        {
            controller.ShowPopup();

            yield return new WaitForSeconds(2.0f);
            controller.HidePopup();
        }

    }

    

}

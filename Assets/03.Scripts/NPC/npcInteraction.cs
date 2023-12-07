using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;


public class NpcInteraction : MonoBehaviour
{
    public NPCSO npc;
    public QuestSO quest;

    [SerializeField] List<ItemSO> allItems;
    private QuestController controller;

    public static UIManager Instance;


    public GameObject dialogueUI;
    public GameObject interactionPopup;


    public TMP_Text nameText;
    public TMP_Text dialogueText;

    //progressui
    //private TMP_Text questProgName;
    QuestProgress questProgress;



    private bool isUIVisible = false;
    private bool isTalking = false;
    Transform player;

    public float activationDistance = 5f;


    void Start()
    {
        controller = FindObjectOfType<QuestController>();
        questProgress = UIManager.Instance.GetQuestProgress();
        if (questProgress == null)
        {
            Debug.LogError("QuestProgress 컴포넌트를 찾을 수 없습니다.");
        }


        dialogueUI.SetActive(false);
        player = GameManager.Instance.Myplayer.transform;

    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance)
        {


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
                    // UIManager.Instance.ActivePlayerUI(true);
                }
            }
        }
        else
        {
            HideDialogue();
            // UIManager.Instance.ActivePlayerUI(true);
        }
        if (isUIVisible && Input.GetKeyDown(KeyCode.F) && !isTalking)
        {
            HideDialogue();
            // UIManager.Instance.ActivePlayerUI(true);
        }

    }
    void ShowDialogue()
    {

        // UIManager.Instance.ActivePlayerUI(false);

        isTalking = true;
        StopAllCoroutines();

        dialogueUI.SetActive(true);
        nameText.text = npc.npcName;



        if (npc.questType == Define.QuestType.ConversationQuest)
        {
            if (npc.hasQuest)
            {
                string appropriateDialogue = npc.completeDialogue[0];
                StartCoroutine(DisplayDialogue(appropriateDialogue));

                npc.conversationCount++;
                npc.hasQuest = false;
                Debug.Log("대화횟수 1증가");
                Debug.Log("현재 총 대화수: " + npc.conversationCount);

                Debug.Log("UpdateConversationQuestProgress 메서드 호출");
                UpdateConversationQuestProgress(quest);

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
    public void UpdateConversationQuestProgress(QuestSO selectedQuest)
    {
        Debug.Log("update됨");
        if (selectedQuest.questType == Define.QuestType.ConversationQuest)
        {
            questProgress.questProgconverseName.color = Color.red;
            questProgress.questProgconverseName.fontStyle = FontStyles.Italic | FontStyles.Strikethrough;
            questProgress.questProgconverseName.text = selectedQuest.questName + "\n - " + " 1 / " + selectedQuest.questComplete;

            
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



        Debug.Log("Quest completed!");  




        if (quest != null && quest.questType == Define.QuestType.ConversationQuest)
        {
            Inventory inventory = UIManager.Instance.GetInventory();

            if (inventory != null)
            {
                inventory.Gold += quest.questRewardCoin;
                Debug.Log(quest.questName + "보상으로 " + quest.questRewardCoin + "개의 금화 획득했습니다!");

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
        if (isCompleteDialogue)
        {
            controller.ShowPopup();

            yield return new WaitForSeconds(2.0f);
            controller.HidePopup();
        }

    }



}

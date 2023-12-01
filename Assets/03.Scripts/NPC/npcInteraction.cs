using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;


public class npcInteraction : MonoBehaviour
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
    public TMP_Text questProgName;


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
        if (selectedQuest.questType == Define.QuestType.ConversationQuest)
        {

            foreach (var npc in selectedQuest.relatedNPCs)
            {

                questProgName.text = selectedQuest.questName + "\n - " + " 1 / " + selectedQuest.questComplete;
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



        Debug.Log("Quest completed!");

        questProgName.color = Color.red;
        questProgName.fontStyle |= FontStyles.Italic;
        questProgName.fontStyle |= FontStyles.Strikethrough;




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

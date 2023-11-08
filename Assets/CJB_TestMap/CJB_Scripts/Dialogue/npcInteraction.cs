using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class npcInteraction : MonoBehaviour
{
    public NPCSO npc;
    public QuestSO quest;
    public QuestController controller;

    public GameObject dialogueUI;
    public GameObject completeUI;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    //progressui
    public TMP_Text questProgName;
    public TMP_Text questComplete;

    private bool isUIVisible = false;
    private bool isTalking = false;
    public Transform player;

    public float activationDistance = 5f;

    void Start()
    {
        controller = FindObjectOfType<QuestController>();
        dialogueUI.SetActive(false);
        
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance)
        {
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
                }
            }
        }
        else
        {
            HideDialogue();
        }
        if (isUIVisible && Input.GetKeyDown(KeyCode.F) && !isTalking)
        {
            HideDialogue();
        }

    }
    void ShowDialogue()
    {
        isTalking = true;
        StopAllCoroutines(); //초기화

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
                    // 퀘스트 완료 처리를 수행합니다.
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

                questProgName.text = selectedQuest.questName + "\n - " + npc.conversationCount + " / " + selectedQuest.questComplete;

            }
        }
        
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

        questProgName.color = Color.green;




    }

    void HideDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
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

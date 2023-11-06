
using TMPro;
using UnityEngine;

public class npcInteraction : MonoBehaviour
{
    public NPCSO npc;
    public GameObject dialogueUI;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private bool isUIVisible = false;
    private bool isTalking = false;
    public Transform player;

    public float activationDistance = 5f;

    void Start()
    {
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
            if (npc.conversationCount >= 1)
            {
                string appropriateDialogue = npc.completeDialogue[0];
                StartCoroutine(DisplayDialogue(appropriateDialogue));
                CompleteConversationQuest(npc);
            }
            else
            {
                string appropriateDialogue = npc.npcDialogue[0];
                StartCoroutine(DisplayDialogue(appropriateDialogue));
                IncrementConversationCount(npc);
            }
        }
        else
        {
            string appropriateDialogue = npc.npcDialogue[0];
            StartCoroutine(DisplayDialogue(appropriateDialogue));
        }

        //string appropriateDialogue = npc.GetAppropriateDialogue(false);
        //StartCoroutine(DisplayDialogue(appropriateDialogue));

    }
    private void IncrementConversationCount(NPCSO npc)
    {
        if (npc.hasQuest)
        {
            npc.conversationCount++;
        }

        

    }

    private void CompleteConversationQuest(NPCSO npc)
    {
        npc.conversationCount = 0;

        // 퀘스트 완료 처리를 수행할 코드 작성
        Debug.Log("Quest completed!");
    }

    void HideDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }

    System.Collections.IEnumerator DisplayDialogue(string dialogue) 
    {
        dialogueText.text = ""; 
        foreach (char letter in dialogue.ToCharArray()) 
        {
            dialogueText.text += letter; 
            yield return new WaitForSeconds(0.05f); 
        }
    }

    

}

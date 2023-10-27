using UnityEngine;
using TMPro;
using System.Collections;

public class NPCInteraction : MonoBehaviour
{
    public GameObject dialogueUI; 
    public TextMeshProUGUI npcNameText; 
    public TextMeshProUGUI dialogueText;
    public Transform player;
    private string currentNPC;

    public float interactionDistance = 5f;

    private bool isUIVisible = false;
    private bool isDialogueInProgress = false;

    public enum NPC
    {
        weaponNPC,
        headNPC,
        potionNPC,
        
    }

    
    public System.Collections.Generic.Dictionary<NPC, string[]> npcDialogues = new System.Collections.Generic.Dictionary<NPC, string[]>()
    {
        { NPC.weaponNPC, new string[] {"대화 1", "대화 2, ","대화 3"}},
        { NPC.headNPC, new string[] { "안녕하신가?", "난 이 마을의 촌장이라네", "반갑다네!","퀘스트를 찾고있다면 뒤에 게시판에 가보게." } },
        { NPC.potionNPC, new string[] { "안녕하세요!", "포션을 사러오셨나요?", "어떤 포션을 찾고 있나요?" } },
        
        
    };

    void Start()
    {
        
        dialogueUI.SetActive(false);
    }
    void Update()
    {
 
        float distance = Vector3.Distance(transform.position, player.position);


        if (distance <= interactionDistance)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                // 상호작용할 NPC를 지정합니다. 
                NPC npcToInteract = NPC.headNPC;


                if (isUIVisible)
                {
                    CloseDialogue();
                }
                else
                {
                    Interact(npcToInteract);
                }

            }
        }
        else
        {
            CloseDialogue();
        }
    }

    // NPC와 상호작용하는 메서드
    public void Interact(NPC npc)
    {
        isUIVisible = true;
        isDialogueInProgress = true;
        dialogueUI.SetActive(true);

        // UI에 NPC 이름과 대화 텍스트를 설정합니다.
        npcNameText.text = npc.ToString();
        dialogueText.text = ""; 

        
        StartCoroutine(ShowDialogue(npcDialogues[npc]));
    }

    
    IEnumerator ShowDialogue(string[] lines)
    {
        foreach (string line in lines)
        {
            dialogueText.text += line + " ";
            yield return new WaitForSeconds(.5f); 
        }
        isDialogueInProgress=false;
    }

    
    public void CloseDialogue()
    {
        isUIVisible = false;
        isDialogueInProgress = false;
        dialogueUI.SetActive(false);
    }
}

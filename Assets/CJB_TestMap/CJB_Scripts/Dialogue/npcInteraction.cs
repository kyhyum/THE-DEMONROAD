
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
        dialogueUI.SetActive(true);
        nameText.text = npc.npcName;

        string appropriateDialogue = npc.GetAppropriateDialogue(false);
        StartCoroutine(DisplayDialogue(appropriateDialogue));
        
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

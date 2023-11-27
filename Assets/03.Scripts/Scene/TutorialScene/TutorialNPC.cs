using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNPC : MonoBehaviour
{
    GameManager gameManager;
    Transform player;

    [SerializeField] NPCSO npc;
    [SerializeField] List<QuestSO> quest;

    [SerializeField] GameObject dialogueUI;

    [SerializeField] Button acceptButton;
    [SerializeField] Button cancelButton;

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;

    private bool isUIVisible = false;
    [SerializeField] bool isTalking = false;

    [SerializeField] float activationDistance;

    const float textDelay = 0.05f;
    int index;
    private void Start()
    {
        /*gameManager = GameManager.Instance;
        player = gameManager.Myplayer.transform;*/
        index = 0;
        nameText.text = npc.npcName;
        StartCoroutine(textPrint());
    }
    private IEnumerator textPrint()
    {
        isTalking = true;
        dialogueText.text = null;
        for (int i = 0; i < npc.npcDialogue[index].Length; i++)
        {
            if (isTalking)
            {
                dialogueText.text += npc.npcDialogue[index][i].ToString();
                yield return new WaitForSeconds(textDelay);
            }
            else
            {
                yield break;
            }
        }
        if(index == 3)
        {
            yield return new WaitForSeconds(2f);
            dialogueUI.gameObject.SetActive(false);
        }
    }
    public void ClickTalk()
    {
        if (isTalking)
        {
            EndTalk();
        }
        else
        {
            Talk();
        }
    }
    private void EndTalk()
    {
        isTalking = false;
        dialogueText.text = npc.npcDialogue[index];
    }
    private void Talk()
    {
        isTalking = true;
        index++;
        StartCoroutine(textPrint());
    }

    private void Accept()
    {
        //switch()
    }
    private void Cancel()
    {
        index = 2;
        Talk();
    }
}

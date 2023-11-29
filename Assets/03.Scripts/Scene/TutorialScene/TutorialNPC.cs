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
    public List<QuestSO> quest;

    [SerializeField] GameObject dialogueUI;
    [SerializeField] GameObject npcCanvas;
    GameObject playerUI;

    [SerializeField] Button acceptButton;
    [SerializeField] Button cancelButton;

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;

    private bool isUIVisible = false;

    [SerializeField] bool isClear = false;

    [SerializeField] float activationDistance;
    float distance;

    const float textDelay = 0.05f;

    int lastTalkIndex;

    [SerializeField] int talkIndex;

    PlayerData data;

    Coroutine talkCoroutine;
    private void Awake()
    {
        nameText.text = npc.npcName;
        npcCanvas.SetActive(false);
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        player = gameManager.Myplayer.transform;

        data = gameManager.player.data;

        playerUI = gameManager.uiManager.playerUIObject;

        acceptButton.onClick.AddListener(Accept);
        cancelButton.onClick.AddListener(Cancel);
        activationDistance = 200f;
        for (int i = 0; i < quest.Count; i++)
        {
            if (data.acceptQuest.Contains(quest[i]))
            {
                activationDistance = 5f;
                switch (i)
                {
                    case 0:
                        talkIndex = 5;
                        break;
                    case 1:
                        talkIndex = 6;
                        break;
                    case 2:
                        talkIndex = 7;
                        break;
                    case 3:
                        talkIndex = 8;
                        break;
                    case 4:
                        talkIndex = 9;
                        break;
                }
            }
        }

        talkCoroutine = StartCoroutine(textPrint());
    }
    private void Update()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, player.position);

        if (distance > activationDistance)
        {
            npcCanvas.SetActive(false);
        }

        if (distance <= activationDistance)
        {
            npcCanvas.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                QuestClear(quest[0]);

                StartTalk();
            }
        }
    }
    private IEnumerator textPrint()
    {
        if (talkIndex >= npc.npcDialogue.Length)
        {
            yield break;
        }

        DialogueUISetActive(true);

        dialogueText.text = null;

        lastTalkIndex = talkIndex;

        ButtonSetActive(false);

        for (int i = 0; i < npc.npcDialogue[talkIndex].Length; i++)
        {
            dialogueText.text += npc.npcDialogue[talkIndex][i].ToString();
            yield return new WaitForSeconds(textDelay);
        }

        yield break;
    }
    public void ClickTalk()
    {
        if (talkCoroutine != null)
        {
            EndTalk();
        }
        else
        {
            Talk();
        }
    }
    private void StartTalk()
    {
        talkIndex = lastTalkIndex;
        talkCoroutine = StartCoroutine(textPrint());
    }
    private void EndTalk()
    {
        StopCoroutine(talkCoroutine);
        talkCoroutine = null;
        dialogueText.text = npc.npcDialogue[talkIndex];

        Invoke("UISetActive", 0.5f);
    }
    void UISetActive()
    {
        if (talkIndex == 3)
        {
            lastTalkIndex = 2;
            DialogueUISetActive(false);
        }
        else if (talkIndex == 5)
        {
            activationDistance = 5f;
            DialogueUISetActive(false);
        }
        else if ((talkIndex >= 6 && talkIndex <= 9) || talkIndex == 2)
        {
            ButtonSetActive(true);
        }
        else
        {
            return;
        }
    }
    private void Talk()
    {
        talkIndex++;
        talkCoroutine = StartCoroutine(textPrint());
    }
    private void DialogueUISetActive(bool active)
    {
        dialogueUI.SetActive(active);
        playerUI.SetActive(!active);
    }
    private void ButtonSetActive(bool active)
    {
        acceptButton.gameObject.SetActive(active);
        cancelButton.gameObject.SetActive(active);
    }

    private void Accept()
    {
        switch (talkIndex)
        {
            case 2:
                data.acceptQuest.Add(quest[0]);
                talkIndex++;
                Talk();
                break;
            case 6:
                QuestAccept(quest[1]);
                break;
            case 7:
                QuestAccept(quest[2]);
                break;
            case 8:
                QuestAccept(quest[3]);
                break;
            case 9:
                QuestAccept(quest[4]);
                break;
        }
    }
    private void Cancel()
    {
        talkIndex = 2;
        Talk();
    }
    public void QuestClear(QuestSO quest)
    {
        if (!data.acceptQuest.Contains(quest))
        {
            return;
        }

        isClear = true;

        switch (quest.questIndex)
        {
            case 990:
                if (isClear)
                {
                    lastTalkIndex = 6;
                    ClearQuestEvent(quest);
                }
                break;
            default:
                if (isClear)
                {
                    lastTalkIndex++;
                    ClearQuestEvent(quest);
                }
                break;
        }
        isClear = false;
    }
    void QuestAccept(QuestSO quest)
    {
        data.acceptQuest.Add(quest);
        DialogueUISetActive(false);
    }
    void ClearQuestEvent(QuestSO quest)
    {
        data.acceptQuest.Remove(quest);
    }
}

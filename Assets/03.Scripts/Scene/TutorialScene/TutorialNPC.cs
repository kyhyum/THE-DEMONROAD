using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialNPC : MonoBehaviour
{
    GameManager gameManager;
    Transform player;

    [SerializeField] NPCSO npc;
    [SerializeField] QuestSO quest;

    private QuestController controller;

    public static UIManager Instance;

    public GameObject dialogueUI;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    //progressui
    public TMP_Text questProgName;
    //public TMP_Text questComplete;

    private bool isUIVisible = false;
    private bool isTalking = false;

    public float activationDistance = 5f;
    void Start()
    {
        gameManager = GameManager.Instance;
        player = gameManager.Myplayer.transform;
    }

    void Update()
    {
        
    }
}

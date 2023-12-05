using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] TutorialNPC npc;

    PlayerInputAction InputActions;
    void Start()
    {
        UIManager.Instance.ActivePlayerUI(true);
        InputActions = GameManager.Instance.Myplayer.GetComponent<PlayerInput>().InputActions;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            npc.QuestClear(npc.quest[1]);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            npc.QuestClear(npc.quest[3]);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            npc.QuestClear(npc.quest[4]);
        }

        if (npc.dialogueUI.gameObject.activeSelf)
        {
            InputActions.Disable();
        }
        else
        {
            InputActions.Enable();
        }
    }

    public void EndTutorialPopUpUI()
    {
        UIManager.Instance.ActivePopUpUI("튜토리얼", "튜토리얼을 종료 하시겠습니까?", EndTutorial);
    }
    public void EndTutorial()
    {
        InputActions.Enable();
        GameManager.Instance.data.acceptQuest.Clear();
        GameManager.Instance.data.currentPlayerPos = Vector3.zero;
        SceneLoadManager.LoadScene((int)Define.SceneType.Town);
    }
}

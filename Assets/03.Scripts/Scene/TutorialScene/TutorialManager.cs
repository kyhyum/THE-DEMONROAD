using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] TutorialNPC npc;
    [SerializeField] AudioClip clip;
    void Start()
    {
        UIManager.Instance.ActivePlayerUI(true);
        SoundManager.Instance.BGMPlay(clip);
    }

    void Update()
    {
        if(GameManager.Instance.player == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            npc.QuestClear(npc.quest[1]);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            npc.QuestClear(npc.quest[3]);
        }
        if (UIManager.Instance.settingObj.activeSelf)
        {
            npc.QuestClear(npc.quest[4]);
        }

        if (npc.dialogueUI.gameObject.activeSelf)
        {
            GameManager.Instance.player.enabled = false;
        }

        else
        {
            GameManager.Instance.player.enabled = true;
        }
    }

    public void EndTutorialPopUpUI()
    {
        if (npc.dialogueUI.gameObject.activeSelf)
        {
            return;
        }

        UIManager.Instance.ActivePopUpUI("튜토리얼", "튜토리얼을 종료 하시겠습니까?", EndTutorial);
    }
    public void EndTutorial()
    {
        GameManager.Instance.player.enabled = true;
        GameManager.Instance.data.acceptQuest.Clear();
        GameManager.Instance.data.currentPlayerPos = Vector3.zero;
        SceneLoadManager.LoadScene((int)Define.SceneType.Town);
    }
}

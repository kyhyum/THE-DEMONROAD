using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;
    [SerializeField] TutorialNPC npc;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.uiManager;
        player = gameManager.Myplayer.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            npc.QuestClear(npc.quest[1]);
            Debug.Log("Q");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            npc.QuestClear(npc.quest[3]);
            Debug.Log("I");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            npc.QuestClear(npc.quest[4]);
            Debug.Log("E");
        }

        if(npc.dialogueUI.gameObject.activeSelf)
        {
            player.enabled = false;
        }
        else
        {
            player.enabled = true;
        }
    }

    public void EndTutorialPopUpUI()
    {
        uiManager.ActivePopUpUI("튜토리얼", "튜토리얼을 종료 하시겠습니까?", EndTutorial);
    }
    private void EndTutorial()
    {
        gameManager.player.currentPlayerPos = Vector3.zero;
        SceneLoadManager.LoadScene((int)SceneType.Town);
    }
}

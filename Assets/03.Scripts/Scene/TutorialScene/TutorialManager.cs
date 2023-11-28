using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.uiManager;
    }

    void Update()
    {
        
    }

    public void EndTutorialPopUpUI()
    {
        uiManager.ActivePopUpUI("튜토리얼", "튜토리얼을 종료 하시겠습니까?", EndTutorial);
    }
    private void EndTutorial()
    {
        gameManager.player.currentPlayerPos = Vector3.zero;
        SceneLoadManager.LoadScene((int)SceneType.Start);
    }
}

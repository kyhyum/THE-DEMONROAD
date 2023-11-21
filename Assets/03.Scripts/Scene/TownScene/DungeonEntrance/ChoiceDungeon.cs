using System;
using UnityEngine;

public class ChoiceDungeon : MonoBehaviour
{
    [SerializeField] GameObject dungeonUI;


    [SerializeField] DungeonExplanUI dungeonExplanUI;

    public GameObject dungeoninteractionPopup;
    private bool isUIVisible = false;
    Transform player;

    //이벤트
    public static event Action DungeonInteractionPopupActivated;

    float activationDistance = 5f;

    void Start()
    {
        player = GameManager.Instance.Myplayer.transform;
        dungeonUI.SetActive(false);
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);


        if (distance <= activationDistance)
        {
            dungeoninteractionPopup.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.F))
            {

                isUIVisible = !isUIVisible;
                dungeonUI.SetActive(isUIVisible);

                if (isUIVisible && DungeonInteractionPopupActivated != null)
                {
                    DungeonInteractionPopupActivated(); 
                }
            }
        }
        else
        {
            isUIVisible = false;
            dungeoninteractionPopup.SetActive(false);
            dungeonExplanUI.gameObject.SetActive(isUIVisible);
            dungeonUI.SetActive(isUIVisible);
        }
    }
    public void OpenDungeon(DungeonSO dungeon)
    {
        dungeonExplanUI.OpenExplan(dungeon);
    }

    public bool IsDungeonInteractionPopupActive()
    {
        return dungeoninteractionPopup.activeSelf;
    }
}

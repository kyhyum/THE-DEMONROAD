using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDungeon : MonoBehaviour
{
    [SerializeField] GameObject dungeonUI;


    [SerializeField] DungeonExplanUI dungeonExplanUI;

    private bool isUIVisible = false;
    Transform player;

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

            if (Input.GetKeyDown(KeyCode.F))
            {

                isUIVisible = !isUIVisible;


                dungeonUI.SetActive(isUIVisible);
            }
        }
        else
        {
            isUIVisible = false;
            dungeonExplanUI.gameObject.SetActive(isUIVisible);
            dungeonUI.SetActive(isUIVisible);
        }
    }
    public void OpenDungeon(DungeonSO dungeon)
    {
        dungeonExplanUI.OpenExplan(dungeon);
    }
}

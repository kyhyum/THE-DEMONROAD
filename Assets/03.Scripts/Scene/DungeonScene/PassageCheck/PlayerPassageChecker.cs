using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPassageChecker : MonoBehaviour
{
    [SerializeField] private List<FadeOutObject> fadeOutObjects;
    [SerializeField] private int passageNum;
    private DungeonManager dungeonManager;
    private BoxCollider boxCollider;
    private void Start()
    {
        dungeonManager = DungeonManager.Instance;
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dungeonManager.CheckAllMonster())
            {
                boxCollider.enabled = false;
                for (int i = 0; i < fadeOutObjects.Count; i++)
                {
                    fadeOutObjects[i].FadeOut();
                }
                dungeonManager.spawnIdx++;
                if (dungeonManager.spawnList.Count == dungeonManager.spawnIdx)
                {
                    //TODO : 클리어 일단 시간 멈춰놓기
                    //Time.timeScale = 0;
                }
                else
                {
                    dungeonManager.Spawn();
                    dungeonManager.SetPlayerPosition(passageNum);
                }
                
            }
        }
    }

}

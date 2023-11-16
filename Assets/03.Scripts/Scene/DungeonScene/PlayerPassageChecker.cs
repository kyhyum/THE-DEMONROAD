using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPassageChecker : MonoBehaviour
{
    public FadeOutObject fadeOutObject { get; private set; }
    private DungeonManager dungeonManager;
    private void Start()
    {
        dungeonManager = DungeonManager.Instance;
        fadeOutObject = new FadeOutObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dungeonManager.CheckAllMonster())
            {
                fadeOutObject.OnEnable();
                dungeonManager.spawnIdx++;
                if (dungeonManager.spawnList.Count == dungeonManager.spawnIdx)
                {
                    //TODO : 클리어 일단 시간 멈춰놓기
                    Time.timeScale = 0;
                }
                else
                {
                    dungeonManager.Spawn();
                }
                
            }
        }
    }
    public void SetDungeonManager(DungeonManager manager)
    {
        dungeonManager = manager;
    }

}

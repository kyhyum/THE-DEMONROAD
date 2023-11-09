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
        fadeOutObject = new FadeOutObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dungeonManager.CheckAllMonster())
            {
                fadeOutObject.OnEnable();
            }
        }
    }
    public void SetDungeonManager(DungeonManager manager)
    {
        dungeonManager = manager;
    }

}

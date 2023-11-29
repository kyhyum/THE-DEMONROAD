using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMob : MonoBehaviour
{
    [SerializeField] TutorialNPC npc;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            npc.QuestClear(npc.quest[1]);
        }
    }
}

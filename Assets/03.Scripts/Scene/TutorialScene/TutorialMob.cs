using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMob : MonoBehaviour, ITakeDamage
{
    [SerializeField] TutorialNPC npc;
    public void TakeDamage(float damage)
    {
        npc.QuestClear(npc.quest[2]);
        Debug.Log("h");
    }
}

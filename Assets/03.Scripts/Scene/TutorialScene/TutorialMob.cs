using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMob : MonoBehaviour
{
    [SerializeField] TutorialNPC npc;
    public void OnTriggerEnter(Collider collider)
    {
        if(collider == GameManager.Instance.player.WeaponCollider)
        {
            npc.QuestClear(npc.quest[2]);
            npc.navUI.SetActive(false);
        }
    }
}

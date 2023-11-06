using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCList", menuName = "NPC System/NPC List")]
public class NPCListSO : ScriptableObject
{
    public List<NPCSO> npcList;
}

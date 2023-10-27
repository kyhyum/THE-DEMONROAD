using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC System/NPC")]
public class NPCSO : ScriptableObject
{
    [field: SerializeField] public string npcName { get; private set; }
    [field: SerializeField] public string[] npcTalk { get; private set; } 
    
}

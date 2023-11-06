using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC System/NPC")]
public class NPCSO : ScriptableObject
{
    [field: SerializeField] public string npcName { get; private set; }
    [field: SerializeField] public string[] npcDialogue { get; private set; }
    [field: SerializeField] public string[] completeDialogue { get; private set; }
    [field: SerializeField] public bool hasQuest { get; private set; }
    public string GetAppropriateDialogue(bool playerHasCompletedQuest)
    {
        if (hasQuest)
        {
            if (playerHasCompletedQuest)
            {
                return completeDialogue[0];
            }
            else
            {
                return npcDialogue[0];
            }
        }
        else
        {
            return npcDialogue[0];
        }
    }
}

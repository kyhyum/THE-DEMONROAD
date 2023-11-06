using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC System/NPC")]
public class NPCSO : ScriptableObject
{
    [field: SerializeField] public string npcName { get; private set; }
    [field: SerializeField] public string[] npcDialogue { get; private set; }
    [field: SerializeField] public string[] completeDialogue { get; private set; }
    [field: SerializeField] public bool hasQuest { get; set; } // 대화퀘스트를 위한 퀘스트의 소지여부

    [field: SerializeField] public QuestType questType { get; private set; }
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

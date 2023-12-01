using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class QuestSO : ScriptableObject
{
    [field: SerializeField] public int questIndex { get; private set; }
    [field: SerializeField] public string questName { get; private set; }
    [field: SerializeField] public Define.QuestType questType { get; set; }
    [field: SerializeField] public string questDescription { get; private set; }
    [field: SerializeField] public string questCondition { get; private set; } //퀘스트 완료조건
    [field: SerializeField] public int questComplete { get; private set; } //퀘스트 완료 숫자
    [field: SerializeField] public string questReward { get; private set; }
    [field: SerializeField] public int questRewardCoin { get; private set; }

    public List<NPCSO> relatedNPCs = new List<NPCSO>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest List", menuName = "Quest System/Quest List")]
public class QuestList : ScriptableObject
{
    public List<QuestSO> quests = new List<QuestSO>();
}



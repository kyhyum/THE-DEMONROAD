using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestList questList;
    public QuestUIManager questUIManager;

    public void AddQuest(QuestSO newQuest)
    {
        questList.quests.Add(newQuest);
        questUIManager.UpdateQuestUI(newQuest);

    }

    public void RemoveQuest(QuestSO quest)
    {
        questList.quests.Remove(quest);
    }
}

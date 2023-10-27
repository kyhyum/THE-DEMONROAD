using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestSO quest;
    public bool isCleared;

    public Quest(QuestSO quest)
    {
        this.quest = quest;
        this.isCleared = false;
    }
}

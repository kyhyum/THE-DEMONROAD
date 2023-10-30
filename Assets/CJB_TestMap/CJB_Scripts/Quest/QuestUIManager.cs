
using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    
    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text questConditionText;
    public TMP_Text questRewardText;

    public QuestManager questManager;

    public void UpdateQuestUI(QuestSO quest)
    {
        
        questNameText.text = quest.questName;
        questDescriptionText.text = quest.questDescription;
        questConditionText.text = quest.questCondition;
        questRewardText.text = quest.questReward;
    }
}

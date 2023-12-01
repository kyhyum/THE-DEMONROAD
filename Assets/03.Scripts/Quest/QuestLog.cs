using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class QuestLog : MonoBehaviour
{
    //gameobject 
    public GameObject questLogPanel;

    //questlog
    [SerializeField] GameObject questLogBackGround;
    [SerializeField] GameObject questLog;
    AcceptQuest acceptQuest;
    public TMP_Text questLogSelected;
    public TMP_Text questLogDescription;
    public TMP_Text questLogRewards;

    GameObject obj;
    private void Start()
    {
        UpdateQuestLogUI();
    }
    private void UpdateQuestLogUI()
    {
        if (GameManager.Instance.data != null && GameManager.Instance.data.acceptQuest != null)
        {
            foreach (var acceptedQuest in GameManager.Instance.data.acceptQuest)
            {
                if (acceptedQuest != null)
                {
                    obj = Instantiate(questLog, questLogBackGround.transform);
                    acceptQuest = obj.GetComponent<AcceptQuest>();
                    acceptQuest.questName.text = acceptedQuest.questName;
                    acceptQuest.questSO = acceptedQuest;
                    UIManager.Instance.GetQuestProgress().ShowQuestProgress(acceptedQuest);
                }
                else
                {
                    Debug.LogWarning("acceptedQuest가 null입니다.");
                }
            }

        }
        else
        {
            Debug.LogWarning("player 또는 player.acceptQuest가 null입니다.");
        }
    }
    public void AddQuestLog(QuestSO questSO)
    {
        if (GameManager.Instance.data != null && GameManager.Instance.data.acceptQuest != null)
        {
            if (GameManager.Instance.data.acceptQuest.Contains(questSO))
            {
                obj = Instantiate(questLog, questLogBackGround.transform);
                acceptQuest = obj.GetComponent<AcceptQuest>();
                acceptQuest.questName.text = questSO.questName;
                acceptQuest.questSO = questSO;
            }
        }
                
    }
    public void ShowLogQuestDetails(QuestSO selectedQuest) 
    {

        questLogSelected.text = selectedQuest.questName;
        questLogDescription.text = selectedQuest.questDescription;
        questLogRewards.text = selectedQuest.questReward;
    }

    public void ShowQuestProgress()
    {
        UIManager.Instance.ActiveQuesProgress();
    }
}
    


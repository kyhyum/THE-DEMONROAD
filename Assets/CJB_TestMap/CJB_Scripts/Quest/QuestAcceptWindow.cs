
using TMPro;
using UnityEngine;

public class QuestAcceptWindow : MonoBehaviour
{
    
    public GameObject acceptWindow;
    public GameObject questLog;

    public TMP_Text questLogList;
    public TMP_Text questSelected;
    public TMP_Text questDescription;
    public TMP_Text questRewards;
   
    public void CloseWindow()
    {
        acceptWindow.SetActive(false);
    }
    public void AcceptQuest()
    {
        CloseWindow();
        questLog.SetActive(true);
        
    }
    

 }

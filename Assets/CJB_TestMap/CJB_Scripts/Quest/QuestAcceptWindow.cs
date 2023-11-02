
using System;
using System.Collections.Generic;
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

    
    public List<QuestSO> acceptedQuests = new List<QuestSO>();

    public void CloseWindow()
    {
        acceptWindow.SetActive(false);
    }
    

    


}

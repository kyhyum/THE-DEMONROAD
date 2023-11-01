using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public GameObject questLogPanel;
    private bool isLogVisible = false;

    private void Start()
    {
        questLogPanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isLogVisible = !isLogVisible;
            questLogPanel.SetActive(isLogVisible);
        }   
    }
}

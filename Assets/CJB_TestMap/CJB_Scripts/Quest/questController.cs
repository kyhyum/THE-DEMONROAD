using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questController : MonoBehaviour
{
    public GameObject questacceptPanel; 

    
    public bool isUIVisible = false;

    void Start()
    {
        questacceptPanel.SetActive(false);
    }

    public void ToggleQuestPanelVisibility()
    {
        isUIVisible = !isUIVisible;
        questacceptPanel.SetActive(isUIVisible);
    }
}

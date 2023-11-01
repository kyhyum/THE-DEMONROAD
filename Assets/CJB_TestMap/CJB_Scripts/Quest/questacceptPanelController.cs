using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questacceptPanelController : MonoBehaviour
{
    public GameObject questacceptPanel; 

    
    private bool isUIVisible = false;

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

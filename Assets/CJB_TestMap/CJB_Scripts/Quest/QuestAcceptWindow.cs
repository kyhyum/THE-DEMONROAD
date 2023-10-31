using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestAcceptWindow : MonoBehaviour
{
   
    public GameObject acceptWindow;
   
    public void CloseWindow()
    {
        acceptWindow.SetActive(false);
    }
}

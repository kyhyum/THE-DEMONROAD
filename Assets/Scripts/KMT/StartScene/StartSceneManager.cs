using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager s_instance;
    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas;
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void OpenStartCanvas()
    {
        startCanvas.SetActive(true);
        selectCanvas.SetActive(false);
        createCanvas.SetActive(false);
    }
    public void OpenCreateCanvas()
    {
        createCanvas.SetActive(true);
        selectCanvas.SetActive(false);
        startCanvas.SetActive(false);
    }
    public void OpenSelectCanvas()
    {
        selectCanvas.SetActive(true);
        createCanvas.SetActive(false);
        startCanvas.SetActive(false);
    }
}
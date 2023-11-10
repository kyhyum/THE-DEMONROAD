using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager Instance;
    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
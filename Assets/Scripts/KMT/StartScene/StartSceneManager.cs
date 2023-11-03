using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager s_instance;
    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas;
    public GameObject[] baseCharacters;

    
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

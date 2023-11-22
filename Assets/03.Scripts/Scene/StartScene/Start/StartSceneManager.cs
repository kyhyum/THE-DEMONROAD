using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager Instance;
    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas, fadeOutCanvas, CreditCanvas, rootObject;
    [SerializeField] Camera mainCamera;
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
        fadeOutCanvas.SetActive(true);
        startCanvas.SetActive(true);
        selectCanvas.SetActive(false);
        createCanvas.SetActive(false);
        mainCamera.gameObject.SetActive(false);
    }
    public void OpenCreateCanvas()
    {
        fadeOutCanvas.SetActive(true);
        createCanvas.SetActive(true);
        mainCamera.gameObject.SetActive(true);
        selectCanvas.SetActive(false);
        startCanvas.SetActive(false);
    }
    public void OpenSelectCanvas()
    {
        fadeOutCanvas.SetActive(true);
        selectCanvas.SetActive(true);
        createCanvas.SetActive(false);
        startCanvas.SetActive(false);
        mainCamera.gameObject.SetActive(false);
    }
    public void OpenCreditCanvas()
    {
        CreditCanvas.SetActive(true);
        rootObject.SetActive(false);
    }
}
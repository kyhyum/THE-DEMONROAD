using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : Singleton<StartSceneManager>
{

    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas, fadeOutCanvas, CreditCanvas, rootObject;
    [SerializeField] Camera mainCamera;
    [SerializeField] Button settingButton, exitButton;
    [SerializeField] AudioClip clip;
    private void Start()
    {
        SoundManager.Instance.BGMPlay(clip);
        settingButton.onClick.AddListener(UIManager.Instance.ActiveSettingWindow);
        exitButton.onClick.AddListener(GameManager.Instance.FinishPopUp);
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
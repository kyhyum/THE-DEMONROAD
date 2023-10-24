using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager s_instance;
    public CharacterSlot characterSlot;
    [SerializeField] GameObject selectCanvas, createCanvas;
    [SerializeField] GameObject[] jobImage;
    public InputField nameCreate;
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
    public void StartButon()
    {
        if (characterSlot.character != null)
        {
            Debug.Log("게임 시작");
        }
        else
        {
            Debug.Log("캐릭터 선택해줘");
        }
    }
    public void OpenCreateCanvas()
    {
        createCanvas.SetActive(true);
        selectCanvas.SetActive(false);
    }
    public void ChangeJobImage(int index)
    {
        for(int i = 0; i < jobImage.Length; i++)
        {
            jobImage[i].SetActive(false);
            if(i == index)
            {
                jobImage[i].SetActive(true);
            }
        }
    }
}

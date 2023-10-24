using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager s_instance;
    public CharacterSlot characterSlot;
    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas;
    [SerializeField] CharacterCreate[] job;
    public int selectJobIndex;
    public TMP_InputField nameCreate;
    
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
    public void OpenSelectCanvas()
    {
        selectCanvas.SetActive(true);
        createCanvas.SetActive(false);
        startCanvas.SetActive(false);
    }
    public void ChangeJob()
    {
        for(int i = 0; i < job.Length; i++)
        {
            job[i].jobImage.SetActive(false);
            job[i].jobCharacter.SetActive(false);
            if (i == selectJobIndex)
            {
                job[i].jobImage.SetActive(true);
                job[i].jobCharacter.SetActive(true);
            }
        }
    }
    public void CreateCharacter()
    {
        string nameText = nameCreate.text;
        string prefabPath = "Assets/Resources/MyCharacter/" + nameText + ".prefab";
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(job[selectJobIndex].jobCharacter, prefabPath);
        characterSlot.character = prefab;
        OpenSelectCanvas();
    }
}

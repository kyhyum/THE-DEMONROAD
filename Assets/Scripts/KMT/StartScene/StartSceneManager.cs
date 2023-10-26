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
    List<string> playerName = new List<string>();
    PlayerData createCharacterData;
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
        GameObject[] objs = Resources.LoadAll<GameObject>("MyCharacter/");
        for(int i = 0; i < objs.Length; i++)
        {
            playerName.Add(objs[i].name);
        }
    }
    private void Start()
    {
        
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
        characterSlot.character = job[0].jobCharacter;
        createCharacterData = job[0].playerData;
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
                characterSlot.character = job[i].jobCharacter;
                createCharacterData = job[i].playerData;
            }
        }
    }
    public void CreateCharacter()
    {
        if (!playerName.Contains(nameCreate.text))
        {
            string nameText = nameCreate.text;
            createCharacterData.name = nameText;
            createCharacterData.playerIndex = characterSlot.slotIndex;
            createCharacterData.level = 1;
            string prefabPath = "Assets/Resources/MyCharacter/";
            GameManager.s_instance.SavePlayerDataToJson(prefabPath, createCharacterData.name, createCharacterData);
            characterSlot.character.AddComponent<PlayerData_KMT>().playerData = GameManager.s_instance.LoadPlayerDataFromJson(prefabPath, createCharacterData.name, createCharacterData);
            OpenSelectCanvas();
        }
        else
        {
            Debug.Log("이미 있는 이름입니다");
        }
    }
}

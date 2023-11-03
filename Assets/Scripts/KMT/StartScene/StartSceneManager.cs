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
    public int selectedSlot;
    [SerializeField] CharacterSlot[] characterSlots;
    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas;
    [SerializeField] GameObject[] baseCharacters;
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
        TextAsset[] jsons = Resources.LoadAll<TextAsset>("MyCharacter/");
        if(jsons.Length > 0)
        {
            for (int i = 0; i < jsons.Length; i++)
            {
                playerName.Add(jsons[i].name);
            }
        }
        string prefabPath = "Assets/Resources/MyCharacter/";
        if(playerName.Count > 0)
        {
            foreach (string one in playerName)
            {
                PlayerData data = GameManager.s_instance.LoadPlayerDataFromJson(prefabPath, one);
                characterSlots[data.playerIndex].character = Instantiate(baseCharacters[(int)data.job], characterSlots[data.playerIndex].transform);
                characterSlots[data.playerIndex].character.AddComponent<PlayerCondition>().playerData = data;
            }
        }
    }
    public void StartButon()
    {
        if(selectedSlot == -1)
        {
            Debug.Log("캐릭터를 선택해주세요");
            return;
        }
        if (characterSlots[selectedSlot].character != null)
        {
            GameManager.s_instance.player = characterSlots[selectedSlot].character.GetComponent<PlayerCondition>().playerData;
            SceneManager.LoadScene((int)GameManager.s_instance.player.scene);
            DontDestroyOnLoad(GameManager.s_instance.gameObject);
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
        nameCreate.text = null;
        selectJobIndex = 0;
        job[selectJobIndex].jobCharacter.SetActive(true);
    }
    public void OpenSelectCanvas()
    {
        selectCanvas.SetActive(true);
        createCanvas.SetActive(false);
        startCanvas.SetActive(false);
        selectedSlot = -1;
    }
    public void BackSelectCanvas()
    {
        OpenSelectCanvas();
        job[selectJobIndex].jobCharacter.SetActive(false);
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
        if (!playerName.Contains(nameCreate.text))
        {
            string nameText = nameCreate.text;
            characterSlots[selectedSlot].character = Instantiate(job[selectJobIndex].jobCharacter);
            createCharacterData = job[selectJobIndex].playerData;
            createCharacterData.name = nameText;
            createCharacterData.playerIndex = selectedSlot;
            createCharacterData.level = 1;
            string prefabPath = "Assets/Resources/MyCharacter/";
            GameManager.s_instance.SavePlayerDataToJson(prefabPath, createCharacterData.name, createCharacterData);
            characterSlots[selectedSlot].character.AddComponent<PlayerCondition>().playerData = GameManager.s_instance.LoadPlayerDataFromJson(prefabPath, createCharacterData.name);
            BackSelectCanvas();
        }
        else
        {
            Debug.Log("이미 있는 이름입니다");
        }
    }
    public void DeleteButton()
    {
        if (selectedSlot == -1)
        {
            Debug.Log("캐릭터를 선택해주세요");
            return;
        }
        if (characterSlots[selectedSlot].character != null)
        {
            characterSlots[selectedSlot].DeleteCharacter();
        }
        else
        {
            Debug.Log("캐릭터 선택해줘");
        }
    }
}

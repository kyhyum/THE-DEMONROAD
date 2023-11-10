using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCanvasManager : MonoBehaviour
{
    public static SelectCanvasManager Instance;
    
    public ChangerSlot[] changerSlots;

    int selectedSlot;
    [SerializeField] CharacterSlot[] characterSlots;
    [SerializeField] GameObject[] baseCharacters;
    
    private List<string> playerName = new List<string>();

    private PlayerData[] playerDatas = new PlayerData[4];
    public PlayerData[] PlayerDatas { get { return playerDatas; } }

    GameManager gameManager;

    StartSceneManager startSceneManager;
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
        gameManager = GameManager.Instance;
        startSceneManager = StartSceneManager.Instance;
        TextAsset[] jsons = Resources.LoadAll<TextAsset>("MyCharacter/");
        if (jsons.Length > 0)
        {
            for (int i = 0; i < jsons.Length; i++)
            {
                playerName.Add(jsons[i].name);
            }
            if (playerName.Count > 0)
            {
                foreach (string one in playerName)
                {
                    PlayerData data = gameManager.LoadPlayerDataFromJson(StringManager.JsonPath, one);
                    playerDatas[data.playerIndex] = data;
                    characterSlots[data.playerIndex].CreateCharacter(baseCharacters[(int)data.job], data);
                }
            }
        }
        for(int i = 0; i < characterSlots.Length; i++)
        {
            characterSlots[i].gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        selectedSlot = -1;
    }
    public void StartButon()
    {
        if (selectedSlot == -1)
        {
            Debug.Log("캐릭터를 선택해주세요");
            return;
        }
        characterSlots[selectedSlot].StartCharacter();
    }
    public void DeleteButton()
    {
        if (selectedSlot == -1)
        {
            Debug.Log("캐릭터를 선택해주세요");
            return;
        }
        characterSlots[selectedSlot].DeleteCharacter();
    }
    public void ClickUpButton(int slotIndex)
    {
        if (changerSlots[slotIndex - 1].PlayerData == null)
        {
            changerSlots[slotIndex - 1].SetData(slotIndex - 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, null);
            playerDatas[slotIndex - 1] = playerDatas[slotIndex];
            playerDatas[slotIndex] = null;
        }
        else
        {
            PlayerData data = changerSlots[slotIndex - 1].PlayerData;
            changerSlots[slotIndex - 1].SetData(slotIndex - 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, data);
            playerDatas[slotIndex - 1] = playerDatas[slotIndex];
            playerDatas[slotIndex] = data;
        }
    }
    public void ClickDownButton(int slotIndex)
    {
        if (changerSlots[slotIndex + 1].PlayerData == null)
        {
            changerSlots[slotIndex + 1].SetData(slotIndex + 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, null);
            playerDatas[slotIndex + 1] = playerDatas[slotIndex];
            playerDatas[slotIndex] = null;
        }
        else
        {
            PlayerData data = changerSlots[slotIndex + 1].PlayerData;
            changerSlots[slotIndex + 1].SetData(slotIndex + 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, data);
            playerDatas[slotIndex + 1] = playerDatas[slotIndex];
            playerDatas[slotIndex] = data;
        }
    }
    public void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;
        characterSlots[selectedSlot].ChoiceSlot();
    }
    public void CreateButton(int slotIndex)
    {
        selectedSlot = slotIndex;
        startSceneManager.OpenCreateCanvas();
    }
    public void CreateCharacter(string name, PlayerData data)
    {
        if (!playerName.Contains(name))
        {
            data.name = name;
            data.playerIndex = selectedSlot;
            data.level = 1;
            gameManager.SavePlayerDataToJson(StringManager.JsonPath, data.name, data);
            PlayerData thisdata = gameManager.LoadPlayerDataFromJson(StringManager.JsonPath, data.name);
            characterSlots[selectedSlot].CreateCharacter(baseCharacters[(int)data.job], thisdata);
            playerDatas[thisdata.playerIndex] = thisdata;
            playerName.Add(name);
            startSceneManager.OpenSelectCanvas();
        }
        else
        {
            Debug.Log("이미 있는 이름입니다");
        }
    }
    public void SlotChangeButton()
    {
        for(int i = 0; i< playerDatas.Length; i++)
        {
            if (playerDatas[i] != null)
            {
                gameManager.SavePlayerDataToJson(StringManager.JsonPath, playerDatas[i].name, playerDatas[i]);
            }
        }
        for(int i = 0; i < characterSlots.Length; i++)
        {
            if (playerDatas[i] != null)
            {
                characterSlots[i].ChangeSlot(baseCharacters[(int)playerDatas[i].job], playerDatas[i]);
            }
            else
            {
                characterSlots[i].ClearSlot();
            }
        }
    }
}

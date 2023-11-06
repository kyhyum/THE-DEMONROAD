using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCanvasManager : MonoBehaviour
{
    public static SelectCanvasManager s_instance;
    public int selectedSlot;
    public ChangerSlot[] changerSlots;
    public CharacterSlot createSlot;
    [SerializeField] CharacterSlot[] characterSlots;
    [SerializeField] GameObject[] baseCharacters;
    public PlayerData[] playerDatas;
    public List<string> playerName = new List<string>();
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
                    PlayerData data = GameManager.s_instance.LoadPlayerDataFromJson(StringManager.jsonPath, one);
                    playerDatas[data.playerIndex] = data;
                    characterSlots[data.playerIndex].character = Instantiate(baseCharacters[(int)data.job], characterSlots[data.playerIndex].transform);
                    characterSlots[data.playerIndex].character.SetActive(true);
                    characterSlots[data.playerIndex].character.AddComponent<PlayerCondition>().playerData = data;
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
        if (characterSlots[selectedSlot].character != null)
        {
            GameManager.s_instance.player = characterSlots[selectedSlot].character.GetComponent<PlayerCondition>().playerData;
            SceneManager.LoadScene((int)GameManager.s_instance.player.scene);
            StartCoroutine(GameManager.s_instance.RealTimeSave());
            DontDestroyOnLoad(GameManager.s_instance.gameObject);
        }
        else
        {
            Debug.Log("캐릭터 선택해줘");
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
            if (GameManager.s_instance.DeleteCharacter(StringManager.jsonPath, characterSlots[selectedSlot].character.GetComponent<PlayerCondition>().playerData.name))
            {
                Destroy(characterSlots[selectedSlot].character);
                characterSlots[selectedSlot].TextOpen(false);
                characterSlots[selectedSlot].character = null;
            }
            else
            {
                Debug.Log("실패했습니다");
            }
        }
        else
        {
            Debug.Log("캐릭터 선택해줘");
        }
    }
    public void ClickUpButton(int slotIndex)
    {
        if (changerSlots[slotIndex - 1].PlayerData == null)
        {
            changerSlots[slotIndex - 1].SetData(slotIndex - 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, null);
        }
        else
        {
            PlayerData data = changerSlots[slotIndex - 1].PlayerData;
            changerSlots[slotIndex - 1].SetData(slotIndex - 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, data);
        }
    }
    public void ClickDownButton(int slotIndex)
    {
        if (changerSlots[slotIndex + 1].PlayerData == null)
        {
            changerSlots[slotIndex + 1].SetData(slotIndex + 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, null);
        }
        else
        {
            PlayerData data = changerSlots[slotIndex + 1].PlayerData;
            changerSlots[slotIndex + 1].SetData(slotIndex + 1, changerSlots[slotIndex].PlayerData);
            changerSlots[slotIndex].SetData(slotIndex, data);
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
        createSlot = characterSlots[selectedSlot];
        StartSceneManager.s_instance.OpenCreateCanvas();
    }
}

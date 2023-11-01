using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager s_instance;
    public CharacterSlot characterSlot;
    [SerializeField] CharacterSlot[] characterSlots;
    [SerializeField] GameObject selectCanvas, createCanvas, startCanvas;
    [SerializeField] GameObject[] baseCharacters;
    [SerializeField] CharacterCreate[] job;
    public int selectJobIndex;
    public TMP_InputField nameCreate;
    List<string> playerName = new List<string>();
    PlayerData createCharacterData;
    PlayerData[] MyCharacterDatas = new PlayerData[4];
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
                characterSlots[data.playerIndex].character = baseCharacters[(int)data.job];
                characterSlots[data.playerIndex].character.AddComponent<PlayerData_KMT>().playerData = data;
            }
        }
    }
    private void Start()
    {
        AddDatas();
    }
    void AddDatas()
    {
        for(int i = 0; i < characterSlots.Length; i++)
        {
            MyCharacterDatas[i] = characterSlots[i].character != null ? characterSlots[i].character.GetComponent<PlayerData_KMT>().playerData : null;
        }
    }
    public void StartButon()
    {
        if (characterSlot.character != null)
        {
            GameManager.s_instance.player = characterSlot.character;
            Debug.Log(GameManager.s_instance.player.GetComponent<PlayerData_KMT>().playerData.name);
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
        AddDatas();
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
            createCharacterData.inventory = new Inventory();
            string prefabPath = "Assets/Resources/MyCharacter/";
            GameManager.s_instance.SavePlayerDataToJson(prefabPath, createCharacterData.name, createCharacterData);
            characterSlot.character.AddComponent<PlayerData_KMT>().playerData = GameManager.s_instance.LoadPlayerDataFromJson(prefabPath, createCharacterData.name);
            OpenSelectCanvas();
        }
        else
        {
            Debug.Log("이미 있는 이름입니다");
        }
    }
    public void SavePlayerDataToJson(string jsonPath, PlayerData[] data)
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(data, true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(jsonPath, "MyCharacter.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }
    public PlayerData[] LoadPlayerDataFromJson(string jsonPath, PlayerData[] data)
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(jsonPath, "MyCharacter.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
        data = JsonUtility.FromJson<PlayerData[]>(jsonData);
        return data;
    }
}

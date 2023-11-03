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
        characterSlots[selectedSlot].character =  job[0].jobCharacter;
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
                characterSlots[selectedSlot].character = job[i].jobCharacter;
                createCharacterData = job[i].playerData;
            }
        }
    }
    public void CreateCharacter()
    {
        if (!playerName.Contains(nameCreate.text))
        {
            string nameText = nameCreate.text;
            characterSlots[selectedSlot].character = Instantiate(characterSlots[selectedSlot].character);
            createCharacterData.name = nameText;
            createCharacterData.playerIndex = selectedSlot;
            createCharacterData.level = 1;
            string prefabPath = "Assets/Resources/MyCharacter/";
            GameManager.s_instance.SavePlayerDataToJson(prefabPath, createCharacterData.name, createCharacterData);
            characterSlots[selectedSlot].character.AddComponent<PlayerCondition>().playerData = GameManager.s_instance.LoadPlayerDataFromJson(prefabPath, createCharacterData.name);
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

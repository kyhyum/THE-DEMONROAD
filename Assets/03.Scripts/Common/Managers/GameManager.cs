using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager uiManager;
    public PlayerData player;
    public GameObject Myplayer;

    public EventSystem eventSystem;

    public PlayerCondition conditon;

    SlotItem slot;

    GameObject obj;
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

        slot = new SlotItem();
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(player.name == "Tester")
        {
            obj = Resources.Load<GameObject>(player.baseObjectPath);
            Myplayer = Instantiate<GameObject>(obj, player.currentPlayerPos, player.currentPlayerRot);
            conditon = Myplayer.AddComponent<PlayerCondition>();
            conditon.playerData = player;
            conditon.Initialize();
            uiManager.gameObject.SetActive(true);
            uiManager.GetInventory().Set(LoadItemArrayFromJson(StringManager.TestItemJsonPath, player.name));
            uiManager.GetStorage().Set(LoadItemArrayFromJson(StringManager.TestItemJsonPath, StringManager.TestStorageName));
            return;
        }
        if (player.baseObjectPath != null && scene.buildIndex != (int)SceneType.Start && scene.buildIndex != (int)SceneType.Loading)
        {
            obj = Resources.Load<GameObject>(player.baseObjectPath);
            Myplayer = Instantiate<GameObject>(obj, player.currentPlayerPos, player.currentPlayerRot);
            conditon = Myplayer.AddComponent<PlayerCondition>();
            conditon.playerData = player;
            conditon.Initialize();
            uiManager.gameObject.SetActive(true);
            uiManager.ActivePlayerUI(true);
            uiManager.GetInventory().Set(LoadItemArrayFromJson(StringManager.ItemJsonPath, player.name));
            uiManager.GetStorage().Set(LoadItemArrayFromJson(StringManager.ItemJsonPath, StringManager.StorageName));
        }
    }
    public void FinishPopUp()
    {
        uiManager.ActivePopUpUI("게임 종료", "정말 게임을 종료 하시겠습니까?", Finish);
    }
    void Finish()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
    public void SavePlayerDataToJson(string jsonPath, string characterName, PlayerData data)
    {

        string jsonData = JsonUtility.ToJson(data, true);

        string path = Path.Combine(jsonPath, $"{characterName}.json");

        File.WriteAllText(path, jsonData);
    }
    public PlayerData LoadPlayerDataFromJson(string jsonPath, string characterName)
    {

        string path = Path.Combine(jsonPath, $"{characterName}.json");
        if (File.Exists(path))
        {

            string jsonData = File.ReadAllText(path);

            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            Application.Quit();
            return null;
        }
    }
    public void SaveItemArrayToJson(string jsonPath, string itemArrayName, SlotItem data)
    {
        slot = data;

        string jsonData = JsonUtility.ToJson(slot, true);

        string path = Path.Combine(jsonPath, $"{itemArrayName}.json");

        File.WriteAllText(path, jsonData);
    }
    public SlotItem LoadItemArrayFromJson(string jsonPath, string itemArrayName)
    {

        string path = Path.Combine(jsonPath, $"{itemArrayName}.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<SlotItem>(jsonData);
        }
        return null;
    }
    public bool DeleteCharacter(string jsonPath, string characterName)
    {
        string path = Path.Combine(jsonPath, $"{characterName}.json");
        bool result = File.Exists(path);
        if (result)
        {
            File.Delete(path);
        }
        return result;
    }
    public void GameStart()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneLoadManager.LoadScene((int)player.scene);
    }
    public void HomeButton()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)SceneType.Start)
        {
            SceneLoadManager.LoadScene((int)SceneType.Start);
            Save();
            player = null;
            Myplayer = null;
        }
    }
    public void Save()
    {
        if (player.name == "Tester")
        {
            player.scene = (SceneType)SceneManager.GetActiveScene().buildIndex;
            player.currentPlayerPos = Myplayer.transform.position;
            player.currentPlayerRot = Myplayer.transform.rotation;
            SaveItemArrayToJson(StringManager.TestItemJsonPath, player.name, uiManager.GetInventory().Get());
            SaveItemArrayToJson(StringManager.TestItemJsonPath, StringManager.TestStorageName, uiManager.GetStorage().Get());
            SavePlayerDataToJson(StringManager.TestJsonPath, player.name, player);
            return;
        }
        if (SceneManager.GetActiveScene().buildIndex != (int)SceneType.Start && SceneManager.GetActiveScene().buildIndex != (int)SceneType.Loading)
        {
            player.scene = (SceneType)SceneManager.GetActiveScene().buildIndex;
            player.currentPlayerPos = Myplayer.transform.position;
            player.currentPlayerRot = Myplayer.transform.rotation;
            SaveItemArrayToJson(StringManager.ItemJsonPath, player.name, uiManager.GetInventory().Get());
            SaveItemArrayToJson(StringManager.ItemJsonPath, StringManager.StorageName, uiManager.GetStorage().Get());
        }
        SavePlayerDataToJson(StringManager.JsonPath, player.name, player);
    }
    private void OnApplicationQuit()
    {
        StopAllCoroutines();
        Save();
    }
}

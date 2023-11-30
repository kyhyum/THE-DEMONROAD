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
    public Player player;
    public PlayerData data;
    public PlayerCondition condition;
    public int goblinkillCount = 0; // 고블린 잡은 횟수
    public GameObject Myplayer;
    public EventSystem eventSystem;

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
        if (data.name == "Tester")
        {
            obj = Resources.Load<GameObject>(data.baseObjectPath);
            Myplayer = Instantiate<GameObject>(obj, data.currentPlayerPos, data.currentPlayerRot);
            condition = Myplayer.AddComponent<PlayerCondition>();
            condition.playerData = data;
            condition.Initialize();
            uiManager.gameObject.SetActive(true);
            uiManager.GetInventory().Set(LoadItemArrayFromJson(StringManager.TestItemJsonPath, player.name));
            uiManager.GetStorage().Set(LoadItemArrayFromJson(StringManager.TestItemJsonPath, StringManager.TestStorageName));
            return;
        }

        if (data.baseObjectPath != null && scene.buildIndex != (int)SceneType.Start && scene.buildIndex != (int)SceneType.Loading)
        {
            obj = Resources.Load<GameObject>(data.baseObjectPath);
            Myplayer = Instantiate<GameObject>(obj, data.currentPlayerPos, data.currentPlayerRot);
            player = Myplayer.GetComponent<Player>();
            condition = Myplayer.AddComponent<PlayerCondition>();
            condition.playerData = data;
            condition.Initialize();
            uiManager.gameObject.SetActive(true);
            uiManager.ActivePlayerUI(true);
            uiManager.GetInventory().Set(LoadItemArrayFromJson(StringManager.ItemJsonPath, data.name));
            uiManager.GetStorage().Set(LoadItemArrayFromJson(StringManager.ItemJsonPath, StringManager.StorageName));
            uiManager.GetSkill().Set(player.skills);
        }
    }

    public void FinishPopUp()
    {
        uiManager.ActivePopUpUI("게임 종료", "정말 게임을 종료 하시겠습니까?", Finish);
    }

    void Finish()
    {
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
        SceneLoadManager.LoadScene((int)data.scene);
    }

    public void HomeButton()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)SceneType.Start)
        {
            SceneLoadManager.LoadScene((int)SceneType.Start);
            Save();
            player = null;
            data = null;
            Myplayer = null;
        }
    }

    public void Save()
    {
        if (data.name == "Tester")
        {
            data.scene = (SceneType)SceneManager.GetActiveScene().buildIndex;
            data.currentPlayerPos = Myplayer.transform.position;
            data.currentPlayerRot = Myplayer.transform.rotation;
            SaveItemArrayToJson(StringManager.TestItemJsonPath, data.name, uiManager.GetInventory().Get());
            SaveItemArrayToJson(StringManager.TestItemJsonPath, StringManager.TestStorageName, uiManager.GetStorage().Get());
            SavePlayerDataToJson(StringManager.TestJsonPath, data.name, data);
            return;
        }

        if (SceneManager.GetActiveScene().buildIndex != (int)SceneType.Start && SceneManager.GetActiveScene().buildIndex != (int)SceneType.Loading)
        {
            data.scene = (SceneType)SceneManager.GetActiveScene().buildIndex;
            data.currentPlayerPos = Myplayer.transform.position;
            data.currentPlayerRot = Myplayer.transform.rotation;
            SaveItemArrayToJson(StringManager.ItemJsonPath, player.name, uiManager.GetInventory().Get());
            SaveItemArrayToJson(StringManager.ItemJsonPath, StringManager.StorageName, uiManager.GetStorage().Get());
        }
        SavePlayerDataToJson(StringManager.JsonPath, data.name, data);
    }
    private void OnApplicationQuit()
    {
        StopAllCoroutines();
        Save();
    }

}

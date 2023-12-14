using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public PlayerData data;
    public PlayerCondition condition;

    public int goblinkillCount = 0;
    public GameObject Myplayer;
    public EventSystem eventSystem;

    public Camera playerCamera;
    public CinemachineVirtualCamera virtualCamera;

    SlotItem slot;
    GameObject obj;

    public delegate void GoblinKillCountChanged(int newCount);
    public static event GoblinKillCountChanged OnGoblinKillCountChanged;
    private void Start()
    {

        slot = new SlotItem();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (data.baseObjectPath != null && scene.buildIndex != (int)Define.SceneType.Start && scene.buildIndex != (int)Define.SceneType.Loading)
        {
            CameraSetActive(true);
            if (Myplayer != null)
            {
                Myplayer.transform.position = data.currentPlayerPos;
                Myplayer.SetActive(true);
                return;
            }
            obj = Resources.Load<GameObject>(data.baseObjectPath);
            Myplayer = Instantiate<GameObject>(obj, data.currentPlayerPos, data.currentPlayerRot);
            player = Myplayer.GetComponent<Player>();
            condition = Myplayer.AddComponent<PlayerCondition>();
            condition.playerData = data;
            condition.Initialize();
            UIManager.Instance.CreateInventory();
            UIManager.Instance.CreateStorage();
            UIManager.Instance.CreateQuestLog();
            UIManager.Instance.CreateQuestProgress();
            
            DontDestroyOnLoad(Myplayer);
        }
        else if (Myplayer != null && scene.buildIndex == (int)Define.SceneType.Loading)
        {
            Myplayer.SetActive(false);
        }
        else
        {
            CameraSetActive(false);
        }
    }
    private void CameraSetActive(bool active)
    {
        playerCamera.gameObject.SetActive(active);
        virtualCamera.gameObject.SetActive(active);
    }

    #region DataManagement
    public void SavePlayerDataToJson(string jsonPath, string characterName, PlayerData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        string encryptedJson = AESManager.EncryptString(jsonData);
        string directoryPath = Path.GetDirectoryName(jsonPath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string path = Path.Combine(jsonPath, $"{characterName}.json");

        File.WriteAllText(path, encryptedJson);
    }

    public PlayerData LoadPlayerDataFromJson(string jsonPath, string characterName)
    {
        string path = Path.Combine(jsonPath, $"{characterName}.json");

        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            string jsonData = AESManager.DecryptString(encryptedJson);
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

        string directoryPath = Path.GetDirectoryName(jsonPath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

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
    public bool DeleteCharacter(string characterName)
    {
        string path = Path.Combine(StringManager.JsonPath, $"{characterName}.json");
        bool result = File.Exists(path);
        if (result)
        {
            File.Delete(path);
            DeleteInventory(characterName);
        }
        return result;
    }
    private void DeleteInventory(string characterName)
    {
        string path = Path.Combine(StringManager.ItemJsonPath, $"{characterName}.json");
        bool result = File.Exists(path);
        if (result)
        {
            File.Delete(path);
        }
    }
    void PlayerPosSave()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 4)
        {
            return;
        }
        data.currentPlayerPos = Myplayer.transform.position;
    }
    public void Save()
    {
        if (data == null || data.level == 0)
        {
            return;
        }

        if (SceneManager.GetActiveScene().buildIndex != (int)Define.SceneType.Start && SceneManager.GetActiveScene().buildIndex != (int)Define.SceneType.Loading)
        {
            data.scene = (Define.SceneType)SceneManager.GetActiveScene().buildIndex;
            PlayerPosSave();
            data.currentPlayerRot = Myplayer.transform.rotation;
            data.QuickSlots = UIManager.Instance.GetQuickSlot();
            SaveItemArrayToJson(StringManager.ItemJsonPath, data.name, UIManager.Instance.GetInventory().Get());
            SaveItemArrayToJson(StringManager.ItemJsonPath, StringManager.StorageName, UIManager.Instance.GetStorage().Get());
        }
        SavePlayerDataToJson(StringManager.JsonPath, data.name, data);
    }
    void DataNull()
    {
        player = null;
        data = null;
        Myplayer = null;
        condition = null;
    }
    #endregion DataManagement

    #region GamePlay
    public void GameStart()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneLoadManager.LoadScene((int)data.scene);
    }

    public void HomeButton()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)Define.SceneType.Start)
        {
            Save();
            UIManager.Instance.DestroyInventoryUI();
            UIManager.Instance.SetQuickSlot(null);
            UIManager.Instance.ActivePlayerUI(false);
            UIManager.Instance.DestroyQuestUI();
            Destroy(Myplayer);
            DataNull();
            SceneLoadManager.LoadScene((int)Define.SceneType.Start);
        }
    }
    public void FinishPopUp()
    {
        UIManager.Instance.ActivePopUpUI("게임 종료", "정말 게임을 종료 하시겠습니까?", Finish);
    }

    private void Finish()
    {
        Save();
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Save();
        DataNull();
        StopAllCoroutines();
    }
    public void UpdateGoblinKillCount(int newCount)
    {
        Debug.Log("고블린 카운트 호출" + newCount);
        goblinkillCount = newCount;


        OnGoblinKillCountChanged?.Invoke(goblinkillCount);
    }
    #endregion GamePlay
}

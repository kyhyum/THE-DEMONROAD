using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject settingCanvas;
    public static GameManager s_instance;
    public PlayerData player;
    public GameObject Myplayer;
    private void Awake()
    {
        Debug.Log("01");
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this);
        }
        if (player != null)
        {
            Myplayer = Instantiate(player.baseObject);
        }
    }
    private void OnEnable()
    {
        
    }
    public void Finish()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    public void SavePlayerDataToJson(string jsonPath, string characterName, PlayerData data)
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(data, true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(jsonPath, $"{characterName}.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }
    public PlayerData LoadPlayerDataFromJson(string jsonPath, string characterName)
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(jsonPath, $"{characterName}.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
        return JsonUtility.FromJson<PlayerData>(jsonData);
        
    }
    void Save()
    {
        if(player != null)
        {
            PlayerData data = player;
            string prefabPath = "Assets/Resources/MyCharacter/";
            SavePlayerDataToJson(prefabPath, data.name, data);
        }
    }
    public void SceneLoad()
    {
        
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    public void OnSettingCanvasInputEnable()
    {
        
    }
    private void ActiveSettingCanvas(InputAction.CallbackContext context)
    {
        settingCanvas.SetActive(!settingCanvas.activeSelf);
    }
}

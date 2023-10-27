using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Job
{
    WARRIOR,
    ARCHOR,
    WIZZARD
}
[System.Serializable]
public class PlayerData
{
    public string name;
    public int playerIndex;
    public int level;
    public int sceneIndex;
    public Job job;
    public List<Statss> stats;
    public Vector3 currentPlayerPos;
    public bool isDead;
    public List<ItemDatata> items;
}
[System.Serializable]
public class ItemDatata
{
    public string name;
    public int itemIndex;
    public float hp;
    public int level;
}
[System.Serializable]
public class Statss
{
    public int str;
    public int dex;
    public int Int;
    public int con;
}
public class PlayerData_KMT : MonoBehaviour
{
    public PlayerData playerData;
    private void Start()
    {
        Debug.Log(playerData.name);
    }
    /*[ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void SavePlayerDataToJson(string jsonPath)
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(playerData, true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(jsonPath, "playerData.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);

    }
    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson(string jsonPath)
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(jsonPath, "playerData.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }*/
}

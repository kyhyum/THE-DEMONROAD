using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour
{
    [SerializeField] GameObject managers;
    GameManager gameManager;
    PlayerData tester;
    int scene;
#if UNITY_EDITOR
    // 유니티 에디터에서 실행 중인지 확인합니다.
    bool isRunningInUnityEditor = true;
#else
    // 빌드된 게임에서 실행 중인지 확인합니다.
    bool isRunningInUnityEditor = false;
    Debug.Log("빌드된 게임에서 실행 중입니다.");
#endif
    private void Awake()
    {
        if (isRunningInUnityEditor)
        {
            Instantiate(managers);
        }
        if (SceneManager.GetActiveScene().name == "TestTownScene")
        {
            scene = (int)SceneType.Town;
        }
        else
        {
            scene = (int)SceneType.Dungeon;
        }
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        tester = gameManager.LoadPlayerDataFromJson(StringManager.TestJsonPath, "Tester");
        tester.currentPlayerPos = Vector3.zero;
        gameManager.player.data = tester;
        SceneLoad();
    }
    void SceneLoad()
    {
        DontDestroyOnLoad(gameManager.gameObject);
        SceneManager.LoadScene(scene);
    }

}

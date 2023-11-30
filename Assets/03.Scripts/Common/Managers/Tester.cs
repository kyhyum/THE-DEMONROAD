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
    bool isRunningInUnityEditor = true;

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
        gameManager.data = tester;
        SceneLoad();
    }
    void SceneLoad()
    {
        DontDestroyOnLoad(gameManager.gameObject);
        SceneManager.LoadScene(scene);
    }

}

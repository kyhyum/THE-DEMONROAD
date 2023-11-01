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
public enum Scene
{
    Start,
    Town,
    Dungeon
}
[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public Job job;
    public Stats stats;

    public int playerIndex;
    
    public Scene scene;
    public Vector3 currentPlayerPos;
    
    public bool isDead;
    public Inventory inventory;
}

[System.Serializable]
public class Stats
{
    public int str;
    public int dex;
    public int Int;
    public int con;
}
public class PlayerData_KMT : MonoBehaviour
{
    public PlayerData playerData;
    private void Awake()
    {
        Debug.Log(playerData.name);
    }
}

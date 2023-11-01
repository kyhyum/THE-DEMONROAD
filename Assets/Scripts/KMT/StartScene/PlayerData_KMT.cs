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
public enum StatType
{
    STR,
    DEX,
    INT,
    CON
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
    public List<Stat> stats;
    public GameObject baseObject;

    public int playerIndex;
    
    public Scene scene;
    public Vector3 currentPlayerPos;
    
    public bool isDead;
    public Inventory inventory;
}

[System.Serializable]
public class Stat
{
    public StatType type;
    public int statValue;
}
public class PlayerData_KMT : MonoBehaviour
{
    public PlayerData playerData;
    private void Start()
    {
        Debug.Log(playerData.name);
    }
}

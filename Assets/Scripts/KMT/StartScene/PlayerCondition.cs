using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public Job job;
    public List<Stat> stats;
    public GameObject baseObject;

    public int playerIndex;
    
    public SceneType scene;
    public Vector3 currentPlayerPos;
    
    public bool isDead;
    /*public Item[] inventory;
    public Item[] storage;*/
}

[System.Serializable]
public class Stat
{
    public StatType type;
    public int statValue;
}
public class PlayerCondition : MonoBehaviour
{
    public PlayerData playerData;
    StatType mainStat;
    public float atk;
    public float def;
    public float speed;
    public float currentHp;
    public float maxHp;
    public float currentMp;
    public float maxMp;
    Dictionary<StatType, int> myStats = new Dictionary<StatType, int>();
    private void Start()
    {
        switch (playerData.job)
        {
            case Job.WARRIOR:
                mainStat = StatType.STR;
                break;
            case Job.ARCHOR:
                mainStat = StatType.DEX;
                break;
            case Job.WIZZARD:
                mainStat = StatType.INT;
                break;
        }
        for(int i = 0; i < playerData.stats.Count; i++)
        {
            myStats.Add(playerData.stats[i].type, playerData.stats[i].statValue);
        }
        atk = myStats[mainStat] * 3;
        def = myStats[StatType.DEX] * 0.5f;
        speed = myStats[StatType.DEX] + 10;
        maxHp = myStats[StatType.CON] + myStats[StatType.STR] * 5;
        currentHp = maxHp;
        maxMp = myStats[StatType.INT] * 10;
        currentMp = maxMp;
        for(int i = 0; i < playerData.stats.Count; i++)
        {
            playerData.stats[i].statValue = myStats[playerData.stats[i].type];
        }
    }
}

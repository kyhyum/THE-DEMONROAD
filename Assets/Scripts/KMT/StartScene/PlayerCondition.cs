using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public int exp;
    public Job job;
    public List<Stat> stats;
    public GameObject baseObject;

    public int playerIndex;
    
    public SceneType scene;
    public Vector3 currentPlayerPos;
    public Quaternion currentPlayerRot;
    
    public bool isDead;

    public List<QuestSO> acceptQuest;

    public float atkRatio;
    public float defRatio;
    public float speedRatio;
    public float hpRatio;
    public float mpRatio;

    public PlayerData(Job job)
    {
        switch(job)
        {
            case Job.WARRIOR:
                atkRatio = 4f;
                defRatio = 0.7f;
                speedRatio = 8f;
                hpRatio = 12f;
                mpRatio = 8f;
                break;
            case Job.ARCHOR:
                atkRatio = 3.5f;
                defRatio = 0.4f;
                speedRatio = 10f;
                hpRatio = 8f;
                mpRatio = 9f;
                break;
            case Job.WIZZARD:
                atkRatio = 4f;
                defRatio = 0.3f;
                speedRatio = 7f;
                hpRatio = 7f;
                mpRatio = 12f;
                break;
        }
    }
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

    public const int mainStatRatio = 3;
    public const int statRatio = 1;

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
        StatSynchronization();
    }
    private void LevelUp()
    {
        playerData.exp -= playerData.level;
        playerData.level++;
        StatUp();
        StatSynchronization();
    }
    void StatUp()
    {
        foreach(var stat in myStats.Keys)
        {
            if(stat == mainStat)
            {
                myStats[stat] += mainStatRatio;
            }
            else
            {
                myStats[stat] += statRatio;
            }
        }
    }
    void StatSynchronization()
    {
        atk = myStats[mainStat] * playerData.atkRatio;
        def = myStats[StatType.DEX] * playerData.defRatio;
        speed = myStats[StatType.DEX] + playerData.speedRatio;
        maxHp = myStats[StatType.CON] + myStats[StatType.STR] * playerData.hpRatio;
        currentHp = maxHp;
        maxMp = myStats[StatType.INT] * playerData.mpRatio;
        currentMp = maxMp;
        for (int i = 0; i < playerData.stats.Count; i++)
        {
            playerData.stats[i].statValue = myStats[playerData.stats[i].type];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public int exp;
    public Job job;
    public List<Stat> stats;
    public int skillPoint;
    public int[] skilllevels;
    public string baseObjectPath;
    public int playerIndex;

    public SceneType scene;
    public Vector3 currentPlayerPos;
    public Quaternion currentPlayerRot;

    public bool isDead;

    public List<QuestSO> acceptQuest;
}

[System.Serializable]
public class Stat
{
    public StatType type;
    public int statValue;
}
public class PlayerCondition : MonoBehaviour, ITakeDamage
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
    public int levelExp;

    float atkRatio;
    float defRatio;
    float speedRatio;
    float hpRatio;
    float mpRatio;
    const int mainStatRatio = 3;
    const int statRatio = 1;

    Dictionary<StatType, int> myStats = new Dictionary<StatType, int>();

    public event Action OnDie;

    public delegate void ValueChangedDelegate(float newValue, float maxValue);
    public event ValueChangedDelegate OnHpChanged;
    public event ValueChangedDelegate OnMpChanged;
    public event ValueChangedDelegate OnExpChanged;

    public bool IsDead => currentHp == 0;
    public void Initialize()
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
        for (int i = 0; i < playerData.stats.Count; i++)
        {
            myStats.Add(playerData.stats[i].type, playerData.stats[i].statValue);
        }
        RaitoSet(playerData.job);
        StatSynchronization();
    }
    public void LevelUp()
    {
        playerData.exp -= (playerData.level * 100);
        playerData.level++;
        StatUp();
        StatSynchronization();
    }

    public void SkillLevelChange(int index, bool flag)
    {
        playerData.skilllevels[index] += (flag) ? 1 : -1;
        playerData.skillPoint--;
    }

    void StatUp()
    {
        foreach (var stat in myStats.Keys)
        {
            if (stat == mainStat)
            {
                myStats[stat] += mainStatRatio;
            }
            else
            {
                myStats[stat] += statRatio;
            }
        }
    }
    void RaitoSet(Job job)
    {
        switch (job)
        {
            case Job.WARRIOR:
                atkRatio = 4f;
                defRatio = 0.7f;
                speedRatio = 1.8f;
                hpRatio = 12f;
                mpRatio = 8f;
                break;
            case Job.ARCHOR:
                atkRatio = 3.5f;
                defRatio = 0.4f;
                speedRatio = 2.5f;
                hpRatio = 8f;
                mpRatio = 9f;
                break;
            case Job.WIZZARD:
                atkRatio = 4f;
                defRatio = 0.3f;
                speedRatio = 2f;
                hpRatio = 7f;
                mpRatio = 12f;
                break;
        }
    }
    void StatSynchronization()
    {
        atk = myStats[mainStat] * atkRatio;
        def = myStats[StatType.DEX] * defRatio;
        speed = myStats[StatType.DEX] * speedRatio;
        maxHp = myStats[StatType.CON] + myStats[StatType.STR] * hpRatio;
        currentHp = maxHp;
        maxMp = myStats[StatType.INT] * mpRatio;
        currentMp = maxMp;
        levelExp = playerData.level * 100;
        for (int i = 0; i < playerData.stats.Count; i++)
        {
            playerData.stats[i].statValue = myStats[playerData.stats[i].type];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ITakeDamage>().TakeDamage(atk);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHp == 0) return;

        float result = currentHp - damage;
        currentHp = Mathf.Max(result, 0);

        OnHpChanged?.Invoke(currentHp, maxHp);

        if (currentHp == 0)
            OnDie?.Invoke();

        Debug.Log(currentHp);
    }

    public bool ConsumeMp(float value)
    {
        if (value > currentMp)
            return false;

        currentMp -= value;

        OnMpChanged?.Invoke(currentMp, maxMp);

        return true;
    }

    public void Buff(BuffType buffType, float duration, int value)
    {
        StartCoroutine(CBuff(buffType, duration, value));
    }

    IEnumerator CBuff(BuffType buffType, float duration, int value)
    {
        switch (buffType)
        {
            case BuffType.Atk:
                atk += value;
                break;
            case BuffType.Def:
                def += value;
                break;
            case BuffType.Str:
                myStats[StatType.STR] += value;
                break;
            case BuffType.Dex:
                myStats[StatType.DEX] += value;
                break;
            case BuffType.Int:
                myStats[StatType.INT] += value;
                break;
            case BuffType.Con:
                myStats[StatType.CON] += value;
                break;
        }

        yield return new WaitForSecondsRealtime(duration);

        switch (buffType)
        {
            case BuffType.Atk:
                atk -= value;
                break;
            case BuffType.Def:
                def -= value;
                break;
            case BuffType.Str:
                myStats[StatType.STR] -= value;
                break;
            case BuffType.Dex:
                myStats[StatType.DEX] -= value;
                break;
            case BuffType.Int:
                myStats[StatType.INT] -= value;
                break;
            case BuffType.Con:
                myStats[StatType.CON] -= value;
                break;
        }
    }
}

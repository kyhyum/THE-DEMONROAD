using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerCondition : MonoBehaviour, ITakeDamage
{
    public PlayerData playerData;
    Define.StatType mainStat;

    public float atk;
    public float def;
    public float speed;
    public float currentHp;
    public float maxHp;
    public float regenHp;
    public float currentMp;
    public float maxMp;
    public float regenMp;
    public int levelExp;


    float atkRatio;
    float defRatio;
    float speedRatio;
    float hpRatio;
    float mpRatio;
    const int mainStatRatio = 3;
    const int statRatio = 1;

    Dictionary<Define.StatType, int> myStats = new Dictionary<Define.StatType, int>();

    public event Action OnDie;

    public delegate void ValuesChangedDelegate(float newValue, float maxValue);
    public event ValuesChangedDelegate OnHpChanged;
    public event ValuesChangedDelegate OnMpChanged;
    public event ValuesChangedDelegate OnExpChanged;

    public delegate void ValueChangedDelegate(int value);
    public event ValueChangedDelegate OnSkillPointChanged;

    Coroutine hpRegen;
    Coroutine mpRegen;

    public bool IsDead => currentHp == 0;
    public void Initialize()
    {
        switch (playerData.job)
        {
            case Define.Job.WARRIOR:
                mainStat = Define.StatType.STR;
                break;
            case Define.Job.ARCHOR:
                mainStat = Define.StatType.DEX;
                break;
            case Define.Job.WIZZARD:
                mainStat = Define.StatType.INT;
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
        playerData.exp -= levelExp;
        levelExp += 100;
        playerData.level++;
        playerData.skillPoint++;
        StatUp();
        StatSynchronization();
        OnSkillPointChanged?.Invoke(playerData.skillPoint);
        OnExpChanged?.Invoke(playerData.exp, levelExp);
    }

    public void SkillLevelChange(int index, bool flag)
    {
        playerData.skilllevels[index] += (flag) ? 1 : -1;
        playerData.skillPoint += (flag) ? -1 : 1;

        OnSkillPointChanged?.Invoke(playerData.skillPoint);
    }

    void StatUp()
    {
        foreach (Stat stat in playerData.stats)
        {
            if (stat.type == mainStat)
            {
                stat.statValue += mainStatRatio;
            }
            else
            {
                stat.statValue += statRatio;
            }
        }

        for (int i = 0; i < playerData.stats.Count; i++)
        {

            myStats[playerData.stats[i].type] = playerData.stats[i].statValue;
        }
    }
    void RaitoSet(Define.Job job)
    {
        switch (job)
        {
            case Define.Job.WARRIOR:
                atkRatio = 4f;
                defRatio = 0.7f;
                speedRatio = 1.8f;
                hpRatio = 12f;
                mpRatio = 8f;
                break;
            case Define.Job.ARCHOR:
                atkRatio = 3.5f;
                defRatio = 0.4f;
                speedRatio = 2.5f;
                hpRatio = 8f;
                mpRatio = 9f;
                break;
            case Define.Job.WIZZARD:
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
        StopGenerate();
        atk = myStats[mainStat] * atkRatio;
        def = myStats[Define.StatType.DEX] * defRatio;
        speed = myStats[Define.StatType.DEX] * speedRatio;
        maxHp = myStats[Define.StatType.CON] + myStats[Define.StatType.STR] * hpRatio;
        regenHp = maxHp * 0.001f;
        currentHp = maxHp;
        maxMp = myStats[Define.StatType.INT] * mpRatio;
        regenMp = maxMp * 0.01f; ;
        currentMp = maxMp;
        levelExp = playerData.level * 100;
        for (int i = 0; i < playerData.stats.Count; i++)
        {
            playerData.stats[i].statValue = myStats[playerData.stats[i].type];
        }
        GenerateResource();
    }
    private void StopGenerate()
    {
        if (hpRegen != null)
            StopCoroutine(hpRegen);
        if (mpRegen != null)
            StopCoroutine(mpRegen);
    }
    public void GenerateResource()
    {
        hpRegen = StartCoroutine(CGenerator(Define.RestoreType.HP));
        mpRegen = StartCoroutine(CGenerator(Define.RestoreType.MP));
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
    public void AddExp(int exp)
    {
        playerData.exp += exp;

        OnExpChanged?.Invoke(playerData.exp, levelExp);

        if (playerData.exp >= levelExp)
        {
            LevelUp();
        }
    }
    public bool ConsumeMp(float value)
    {
        if (value > currentMp)
            return false;

        currentMp -= value;

        OnMpChanged?.Invoke(currentMp, maxMp);

        return true;
    }
    public void Buff(Define.BuffType buffType, float duration, int value)
    {
        StartCoroutine(CBuff(buffType, duration, value));
    }
    public void Restore(Define.RestoreType type, float value)
    {
        switch (type)
        {
            case Define.RestoreType.HP:
                currentHp += value;
                currentHp = currentHp > maxHp ? maxHp : currentHp;
                OnHpChanged?.Invoke(currentHp, maxHp);
                break;
            case Define.RestoreType.MP:
                currentMp += value;
                currentMp = currentMp > maxMp ? maxMp : currentMp;
                OnMpChanged?.Invoke(currentMp, maxMp);
                break;
        }
    }
    IEnumerator CGenerator(Define.RestoreType type)
    {
        while (true)
        {
            switch (type)
            {
                case Define.RestoreType.HP:
                    Restore(type, regenHp);
                    break;
                case Define.RestoreType.MP:
                    Restore(type, regenMp);
                    break;
            }
            yield return new WaitForSecondsRealtime(.2f);
        }
    }

    IEnumerator CBuff(Define.BuffType buffType, float duration, int value)
    {
        switch (buffType)
        {
            case Define.BuffType.Atk:
                atk += value;
                break;
            case Define.BuffType.Def:
                def += value;
                break;
            case Define.BuffType.Str:
                myStats[Define.StatType.STR] += value;
                break;
            case Define.BuffType.Dex:
                myStats[Define.StatType.DEX] += value;
                break;
            case Define.BuffType.Int:
                myStats[Define.StatType.INT] += value;
                break;
            case Define.BuffType.Con:
                myStats[Define.StatType.CON] += value;
                break;
        }

        yield return new WaitForSecondsRealtime(duration);

        switch (buffType)
        {
            case Define.BuffType.Atk:
                atk -= value;
                break;
            case Define.BuffType.Def:
                def -= value;
                break;
            case Define.BuffType.Str:
                myStats[Define.StatType.STR] -= value;
                break;
            case Define.BuffType.Dex:
                myStats[Define.StatType.DEX] -= value;
                break;
            case Define.BuffType.Int:
                myStats[Define.StatType.INT] -= value;
                break;
            case Define.BuffType.Con:
                myStats[Define.StatType.CON] -= value;
                break;
        }
    }
}

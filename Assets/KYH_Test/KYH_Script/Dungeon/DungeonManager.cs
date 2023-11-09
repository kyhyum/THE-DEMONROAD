using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    //TODO: 인스턴스화 시켜야 되나?
    public GameObject monsterPrefab;
    public GameObject goblinPrefab;
    public GameObject swordGoblinPrefab;
    public GameObject orkberserkerPrefab;

    public ObjectPool<Monster> monsterObjectPool { get; private set; }
    public ObjectPool<Monster> goblinObjectPool { get; private set; }
    public ObjectPool<Monster> swordGoblinObjectPool { get; private set; }
    public ObjectPool<Monster> orkberserkerObjectPool { get; private set; }

    public List<PlayerPassageChecker> checkerList = new List<PlayerPassageChecker>();
    public List<MonsterSpawn> spawnList = new List<MonsterSpawn>();

    private void Start()
    {
        monsterObjectPool = new ObjectPool<Monster>(monsterPrefab.GetComponent<Monster>(), 10);
        goblinObjectPool = new ObjectPool<Monster>(goblinPrefab.GetComponent<Monster>(), 10);
        swordGoblinObjectPool = new ObjectPool<Monster>(swordGoblinPrefab.GetComponent<Monster>(), 10);
        orkberserkerObjectPool = new ObjectPool<Monster>(orkberserkerPrefab.GetComponent<Monster>(), 10);

        foreach (var checker in checkerList)
        {
            checker.SetDungeonManager(this); // DungeonManager에 대한 참조 전달
        }

        Spawn(0);
    }

    public bool CheckAllMonster()
    {
        return monsterObjectPool.CheckListSize() && goblinObjectPool.CheckListSize()
               && swordGoblinObjectPool.CheckListSize() && orkberserkerObjectPool.CheckListSize();
    }

    public GameObject SpawnMonster(MonsterType monsterType, int spawnListIdx)
    {
        Vector3 spawnPos = spawnList[spawnListIdx].ReturnRandomPosition();
        Monster monster = null;
        switch (monsterType)
        {
            case MonsterType.Goblin:
                monster = goblinObjectPool.GetObject();
                break;
            case MonsterType.SwordGoblin:
                monster = swordGoblinObjectPool.GetObject();
                break;
            case MonsterType.Monster:
                monster = monsterObjectPool.GetObject();
                break;
            case MonsterType.orkBerserk:
                monster = orkberserkerObjectPool.GetObject();
                break;
        }

        monster.EnemyNavMeshAgent.enabled = false;
        monster.gameObject.transform.position = spawnPos;
        monster.EnemyNavMeshAgent.enabled = true;

        return monster.gameObject;
    }

    private void Spawn(int idx)
    {
        for(MonsterType m = 0; (int)m < 4; m++)
        {
            for(int i = 0; i < spawnList[idx].spawnList[(int)m].spawnCount; i++)
            {
                SpawnMonster(m, idx);
            }
        }
    }
}

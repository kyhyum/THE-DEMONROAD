using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    public int spawnIdx = 0;


    [field: Header("Prefabs")]
    [SerializeField] private AudioClip DungeonSound;

    [field: Header("Prefabs")]
    public GameObject monsterPrefab;
    public GameObject goblinPrefab;
    public GameObject swordGoblinPrefab;
    public GameObject orkberserkerPrefab;

    //TODO: 골드, (hp,mp포션), 퀘스트 템, 장비 (Item.class)
    public ObjectPool<Monster> monsterObjectPool { get; private set; }
    public ObjectPool<Monster> goblinObjectPool { get; private set; }
    public ObjectPool<Monster> swordGoblinObjectPool { get; private set; }
    public ObjectPool<Monster> orkberserkerObjectPool { get; private set; }

    [field: Header("Checker")]
    public List<PlayerPassageChecker> checkerList = new List<PlayerPassageChecker>();
    [field: Header("Spawn")]
    public List<MonsterSpawn> spawnList = new List<MonsterSpawn>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //사운드 플레이
        SoundManager.Instance.BGMPlay(DungeonSound);

        monsterObjectPool = new ObjectPool<Monster>(monsterPrefab.GetComponent<Monster>(), 20);
        goblinObjectPool = new ObjectPool<Monster>(goblinPrefab.GetComponent<Monster>(), 10);
        swordGoblinObjectPool = new ObjectPool<Monster>(swordGoblinPrefab.GetComponent<Monster>(), 20);
        orkberserkerObjectPool = new ObjectPool<Monster>(orkberserkerPrefab.GetComponent<Monster>(), 10);

        Spawn();
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
                monster.objectPoolReturn += goblinObjectPool.ReturnObject;
                break;
            case MonsterType.SwordGoblin:
                monster = swordGoblinObjectPool.GetObject();
                monster.objectPoolReturn += swordGoblinObjectPool.ReturnObject;
                break;
            case MonsterType.Monster:
                monster = monsterObjectPool.GetObject();
                monster.objectPoolReturn += monsterObjectPool.ReturnObject;
                break;
            case MonsterType.orkBerserk:
                monster = orkberserkerObjectPool.GetObject();
                monster.objectPoolReturn += orkberserkerObjectPool.ReturnObject;
                break;
        }


        monster.EnemyNavMeshAgent.enabled = false;
        monster.gameObject.transform.position = spawnPos;
        monster.EnemyNavMeshAgent.enabled = true;

        return monster.gameObject;
    }

    public void Spawn()
    {
        for(MonsterType m = 0; (int)m < 4; m++)
        {
            for(int i = 0; i < spawnList[spawnIdx].spawnList[(int)m].spawnCount; i++)
            {
                SpawnMonster(m, spawnIdx);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public MonsterSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public MonsterAnimationData monsterAnimationData { get; private set; }
    public Animator Animator { get; private set; }

    [field: Header("NavMesh")]
    public NavMeshAgent EnemyNavMeshAgent { get; private set; }

    [field: Header("FSM")]

    public MonsterStateMachine stateMachine { get; private set; }

    [field: Header("Monster")]
    public MonsterHealth MonsterHealth { get; private set; }
    [field: SerializeField] public MonsterWeapon Weapon { get; private set; }
    public MonsterSound monsterSound { get; private set; }

    public ItemDropController itemDropController { get; private set; }
    public event Action<Monster> objectPoolReturn;

    private void Awake()
    {
        monsterAnimationData.Initialize();

        Animator = GetComponent<Animator>();
        EnemyNavMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new MonsterStateMachine(this);
        MonsterHealth = GetComponent<MonsterHealth>();
        itemDropController = GetComponent<ItemDropController>();
        monsterSound = GetComponent<MonsterSound>();

    }

    //NavMeshAent 초기화
    private void InitNavMesh()
    {
        EnemyNavMeshAgent.speed = Data.BaseSpeed;
        EnemyNavMeshAgent.autoBraking = false;
    }

    public void GetStun(float time)
    {
        stateMachine.StunState.SetStunTime(time);
        stateMachine.ChangeState(stateMachine.StunState);
    }

    private void Start()
    {
        InitNavMesh();
        InitMonster();
        MonsterHealth.OnDie += OnDie;
        MonsterHealth.OnDie += itemDropController.DropItem;
    }

    public void InitMonster()
    {
        MonsterHealth.InitEnemyHealth(Data.Health, Data.Name);
        stateMachine.ChangeState(stateMachine.IdleState);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        MonsterHealth.OnDie += monsterSound.PlayDeadSound;
        Animator.SetTrigger("Die");
        Invoke("AfterAnimationComplete", Animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void AfterAnimationComplete()
    {
        objectPoolReturn?.Invoke(this);
        InitMonster();
        objectPoolReturn = null;
    }
}

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

    public EnemyForceReceiver EnemyForceReceiver { get; private set; }
    public NavMeshAgent EnemyNavMeshAgent { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private MonsterStateMachine stateMachine;        
    [field: SerializeField] public MonsterWeapon Weapon { get; private set; }
    public MonsterHealth MonsterHealth { get; private set; }

    private void Awake()
    {
        monsterAnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        EnemyForceReceiver = GetComponent<EnemyForceReceiver>();
        EnemyNavMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new MonsterStateMachine(this);
        MonsterHealth = GetComponent<MonsterHealth>();
        MonsterHealth.InitEnemyHealth(Data.Health, Data.Name);

    }

    //NavMeshAent 초기화
    private void InitNavMesh()
    {
        EnemyNavMeshAgent.speed = Data.BaseSpeed;
        EnemyNavMeshAgent.autoBraking = false;
    }

    private void Start()
    {
        InitNavMesh();
        MonsterHealth.health = Data.Health;
        stateMachine.ChangeState(stateMachine.IdleState);
        MonsterHealth.OnDie += OnDie;
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
        Animator.SetTrigger("Die");
        enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public MonsterSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public BossAnimationData bossAnimationData { get; private set; }

    public EnemyForceReceiver EnemyForceReceiver { get; private set; }
    public NavMeshAgent BossNavMeshAgent { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private BossStateMachine stateMachine;        
    [field: SerializeField] public MonsterWeapon Weapon { get; private set; }
    public MonsterHealth MonsterHealth { get; private set; }

    private void Awake()
    {
        bossAnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        EnemyForceReceiver = GetComponent<EnemyForceReceiver>();
        BossNavMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new BossStateMachine(this);
        MonsterHealth = GetComponent<MonsterHealth>();

    }

    //NavMeshAent 초기화
    private void InitNavMesh()
    {
        BossNavMeshAgent.speed = Data.BaseSpeed;
        BossNavMeshAgent.autoBraking = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    
    [field: Header("References")]
    [field: SerializeField] public BossSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public BossAnimationData bossAnimationData { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] private GameObject bossBullet1Obj;
    [field: SerializeField] private GameObject bossBullet2Obj;
    public EnemyForceReceiver EnemyForceReceiver { get; private set; }
    public NavMeshAgent BossNavMeshAgent { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private BossStateMachine stateMachine;        
    [field: SerializeField] public BossWeapon Weapon { get; private set; }
    public BossHealth BossHealth { get; private set; }

    public ObjectPool<BossBullet> pattern1Bullet { get; private set; }
    public ObjectPool<BossBullet> pattern2Bullet { get; private set; }

    private void Awake()
    {
        bossAnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        EnemyForceReceiver = GetComponent<EnemyForceReceiver>();
        BossNavMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new BossStateMachine(this);
        BossHealth = GetComponent<BossHealth>();
        BossHealth.InitEnemyHealth(Data.Health, Data.Name);

        pattern1Bullet = new ObjectPool<BossBullet>(bossBullet1Obj.GetComponent<BossBullet>(), 3);
        pattern2Bullet = new ObjectPool<BossBullet>(bossBullet2Obj.GetComponent<BossBullet>(), 1);
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
        BossHealth.health = Data.Health;
        stateMachine.ChangeState(stateMachine.IdleState);
        BossHealth.OnDie += OnDie;
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

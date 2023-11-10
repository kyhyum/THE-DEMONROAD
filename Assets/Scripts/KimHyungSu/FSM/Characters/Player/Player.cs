// 필요한 데이터들, 컴포넌트들을 사용할 것이다.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public LayerMask groundLayerMask;

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }
    public PlayerCondition playerCondition { get; private set; }

    public Camera Camera { get; private set; }
    public bool IsAttacking { get; set; }

    private PlayerStateMachine stateMachine;


    public Slider hpSlider;

    public Slider mpSlider;


    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Agent.updatePosition = false;
        Agent.updateRotation = true;

        Camera = Camera.main;

        playerCondition = GetComponent<PlayerCondition>();

        Agent.speed = playerCondition.speed;

        stateMachine.ChangeState(stateMachine.IdleState);

        playerCondition.OnDie += OnDie;
        playerCondition.OnHealthChanged += UpdateHealthUI;
        playerCondition.OnManaChanged += UpdateManaUI;

        hpSlider.maxValue = playerCondition.maxHp;
        hpSlider.value = playerCondition.maxHp;
        mpSlider.maxValue = playerCondition.maxMp;
        mpSlider.value = playerCondition.maxMp;

        UIManager.Instance.OnUIInputEnable();
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void LateUpdate()
    {
        stateMachine.LateUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnUIInputDisable();
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");
        // Player.cs를 false로 만든다.
        enabled = false;
    }

    void UpdateHealthUI(float newHealth)
    {
        hpSlider.value = newHealth;
    }

    void UpdateManaUI(float newMana)
    {
        mpSlider.value = newMana;
    }
}

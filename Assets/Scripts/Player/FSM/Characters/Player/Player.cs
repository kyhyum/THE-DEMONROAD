// 필요한 데이터들, 컴포넌트들을 사용할 것이다.

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
    [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; set; }
    [field: SerializeField] public CinemachineComponentBase ComponentBase { get; set; }

    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }
    public bool IsAttackSkill1 { get; set; }

    private PlayerStateMachine stateMachine;

    [field: SerializeField] Transform cameraLookPoint;

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

        VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        if (VirtualCamera != null)
        {
            VirtualCamera.Follow = cameraLookPoint;
        }

        if (ComponentBase == null)
        {
            ComponentBase = VirtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }

    }

    private void Update()
    {
        MousePointerOverUI();

        stateMachine.Update();
    }

    /// <summary>
    /// 마우스 포인터가 UI 위에 있으면 입력을 비활성화하고, UI 위에 없으면 입력을 활성화한다.
    /// </summary>
    void MousePointerOverUI()
    {
        // 마우스 포인터가 UI 위에 있으면
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("마우스 포인터가 UI 위에 있다.");

            Input.InputActions.Disable();
        }
        //
        else
        {
            Debug.Log("마우스 포인터가 UI 위에 없다.");

            Input.InputActions.Enable();
        }
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
